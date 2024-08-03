using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Common.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Services
{
    public class CheckoutService : ICheckoutService
    {
        //public CheckoutService ProcessCheckout()
        //{
        //    return new CheckoutService();
        //}
        private readonly ApplicationDbContext _context;
        
        public CheckoutService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ProcessCheckout(int shoppingCartId)
        {
            var transaction = _context.Database.BeginTransaction();

            try
            {
                transaction.CreateSavepoint("BeforeProcessCheckoutStart");
                var cartItems = _context.CartItems?
                    .Include(x => x.Product)
                    .Include(x=>x.ShoppingCart)
                    .Where(x => x.ShoppingCartId == shoppingCartId)?.ToList();

                var userId = _context.ShoppingCarts.FirstOrDefault(x=>x.Id == shoppingCartId)?.UserId;

                var order = CreateOrder(cartItems);

                if(order is null)
                    return false;

                decimal totalProductPrice = CalculateTotalProductPrice(order);
                var isDiscountedFromWallet=  DiscountFromWallet(userId, totalProductPrice);
                if(!isDiscountedFromWallet)
                    return false;
                order.TotalAmount = totalProductPrice;
                order.UserId = userId;

                var isDeleted =  RemoveCartItems(shoppingCartId);
                if(!isDeleted)
                  return false;
                var isDiscountedProductQuantity =   DiscountProductQuantity(order);
                if(!isDiscountedFromWallet)
                    return false;

                _context.Orders.Add(order);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.RollbackToSavepoint("BeforeProcessCheckoutStart");
                return false;
            }

        }
        private Order CreateOrder(List<CartItem> cartItems)
        {
            var order = new Order
            {
                Status = "Under Preparation",
                ShippingAddress = "",
            };
            foreach (var cartItem in cartItems)
            {
                order.orderItems.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.ProductQuantity,
                    OrderId = 1,
                    UnitPrice = cartItem.Product.Price
                });
            }
            return order;
        }
        private decimal CalculateTotalProductPrice(Order order)
        {
            decimal totalPrice = order.orderItems.Sum(x => (x.Quantity * x.UnitPrice) ?? 0);

            return totalPrice;
        }


        private bool DiscountFromWallet(string userId, decimal totalProductPrice)
        {
            try
            {
                var userWallet = _context.Wallets.SingleOrDefault(x => x.UserId == userId);
                var userBalance = userWallet?.Balance ?? 0;
                if (userBalance < totalProductPrice)
                    return false;

                userWallet.Balance = userBalance - totalProductPrice;
                _context.Wallets.Update(userWallet);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool DiscountProductQuantity(Order order)
        {
            
            foreach(var orderItem in order.orderItems)
            {
                var productId = orderItem.ProductId;
                 var quantity = orderItem.Quantity ?? 0;
                
                var product = _context.Products.Find(productId);
                product.Quantity = product.Quantity - quantity;
                _context.Products.Update(product);

            }
            return true;
        }
        private bool RemoveCartItems (int shoppingCartId)
        {
            try
            {
                var cartItem = _context.CartItems.Where(x => x.ShoppingCartId == shoppingCartId);
                _context.RemoveRange(cartItem);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

    }
}
