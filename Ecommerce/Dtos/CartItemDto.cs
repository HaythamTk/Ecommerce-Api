using Ecommerce.Domain.Models;

namespace Ecommerce.Dtos
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductQuantity { get; set; }
    }
}
