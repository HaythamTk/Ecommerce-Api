using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        private readonly IProduct _product;
        public Products(IProduct product)
        {
            _product = product;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var products = _product.GetAll();
            if (!products.Any())
                return NotFound("No Items");
            return Ok(products);

        }
        [HttpGet("GetProductById")]
        public IActionResult GetProductById(int id)
        {
            if (id <= 0)
                return BadRequest();
            var product = _product.GetProductById(id);
            if (product == null)
                return NotFound();

            return Ok(product);

        }
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromForm] ProductDto productDto)
        {
            if (productDto == null)
                return BadRequest("No Id Send");

            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                CategoryId = productDto.CategoryId,
            };


            var productItem = _product.AddProduct(product);
            if (!productItem)
                return BadRequest("Something Not right");

            return Ok(productItem);
        }
    }
}
