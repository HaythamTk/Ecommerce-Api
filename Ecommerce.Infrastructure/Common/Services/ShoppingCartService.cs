using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Common.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Services
{
    public class ShoppingCartService : IShoppingCart
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public bool AddShoppingCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;
            try
            {
                var shoppingCart = new ShoppingCart
                {
                    UserId = userId,
                };
                _context.ShoppingCarts.Add(shoppingCart);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
