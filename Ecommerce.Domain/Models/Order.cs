using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string? ShippingAddress { get; set; }
        public string UserId { get; set;}
        public ICollection<OrderItem>? orderItems { get; set; } = new List<OrderItem>();
        public ApplicationUser User { get; set; }
    }
}
