using Ecommerce.Domain.Models;

namespace Ecommerce.Dtos
{
    public class OrderDto
    {
        public Decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
