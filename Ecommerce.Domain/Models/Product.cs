using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public int CategoryId { get; set; }

       // [JsonIgnore]
        public Category Category { get; set; }

        [JsonIgnore]
        public ICollection<CartItem>? CartItem { get; set; } = new List<CartItem>();
        public ICollection<OrderItem>? OrderItem { get; set; } = new List<OrderItem>();
    }
}
