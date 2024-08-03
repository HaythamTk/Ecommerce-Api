using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        //public int CartId { get; set; }
        public ICollection<CartItem>? Cart { get; set; } = new List<CartItem>();
        public string? UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
