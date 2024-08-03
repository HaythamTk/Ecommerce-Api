using Ecommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface ICartItem
    {
        public bool AddCartItem(CartItem cartItem);
        public CartItem UpdateCartItem(CartItem cartItem);
        public bool DeleteCartItem(int id);
        public IEnumerable<object> GetAll();
        public CartItem GetCartItemById(int id);
    }
}
