using Ecommerce.Domain.Models;

namespace Ecommerce.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public int CategoryId { get; set; }
       // public Category Category { get; set; }
        //public CartItem CartItem { get; set; }
    }
}
