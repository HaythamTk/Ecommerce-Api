using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsItem : ControllerBase
    {
        private readonly ICartItem _cartItem;
        public CartsItem(ICartItem cartItem)
        {
            _cartItem = cartItem;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var cartsItem = _cartItem.GetAll();
            if (!cartsItem.Any())
                return NotFound("No Items");
            return Ok(cartsItem);

        }
        [HttpGet("GetCartItemById")]
        public IActionResult GetCartItemById(int id)
        {
            if (id <= 0)
                return BadRequest();
            var cartItem = _cartItem.GetCartItemById(id);
            if (cartItem == null)
                return NotFound();

            return Ok(cartItem);

        }
        [HttpPost("AddCartItem")]
        public IActionResult Add([FromForm] CartItemDto cartItemDto)
        {
            if (cartItemDto == null)
                return BadRequest("No Id Send");

            var cart = new CartItem
            {
                ProductId = cartItemDto.ProductId,
                ShoppingCartId = cartItemDto.ShoppingCartId,
                ProductQuantity = cartItemDto.ProductQuantity,
            };
            

            var cartItem = _cartItem.AddCartItem(cart);
            if (!cartItem)
                return BadRequest("Something Not right");

            return Ok(cartItem);
        }


        [HttpPost("DeleteCartItem")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var isDeleted = _cartItem.DeleteCartItem(id);
            if (!isDeleted)
                return NotFound();
            return Ok(isDeleted);

        }
    }
}
