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
    
    public class CartItemService : ICartItem
    {
        private readonly ApplicationDbContext _context;
        public CartItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddCartItem(CartItem cartItem)
        {
            if (cartItem == null)
                return false;

            try
            {
                _context.CartItems.Add(cartItem);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCartItem(int id)
        {
            if (id <= 0)
                return false;

            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<object> GetAll()
        {
            var cartItem = _context.CartItems
            .Include(x => x.Product)
            .Select(z => new { z.Id,z.ShoppingCartId,ProductId = z.Product.Id,ProductName = z.Product.Name, ProductPrice = z.Product.Price, ProductQuantity = z.ProductQuantity })
            .ToList();
            if (cartItem.Any())
                return cartItem;

            return new List<CartItem>();
        }

        public CartItem GetCartItemById(int id)
        {
            if (id > 0)
            {
                var cartItem = new CartItem();
                cartItem = _context.CartItems.Find(id);
                return cartItem;
            }
            return new CartItem();
        }

        public CartItem UpdateCartItem(CartItem cartItem)
        {
            throw new NotImplementedException();
        }
    }
}
