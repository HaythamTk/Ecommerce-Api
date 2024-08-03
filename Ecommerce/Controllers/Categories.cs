using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Dtos;
using Ecommerce.Infrastructure.Common.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Categories : ControllerBase
    {
        private readonly ICategory _category;
        public Categories(ICategory category)
        {
            _category = category;
        }
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categories = _category.GetAll();
            if (!categories.Any())
                return NotFound("No Items");
            return Ok(categories);

        }
        [HttpGet("GetCategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            if (id <= 0)
                return BadRequest();
            var category = _category.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return Ok(category);

        }
        [HttpPost("AddCategory")]
        public IActionResult Add([FromForm] CategoryDto categoryDto)
        {
            if (categoryDto == null)
                return BadRequest("No Id Send");

            var category = new Domain.Models.Category();
            category.Name = categoryDto.Name;

            var categoryItem = _category.AddCategory(category);
            if (!categoryItem)
                return BadRequest("Something Not right");

            return Ok(categoryItem);
        }
       

        [HttpPost("DeleteCategory")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var isDeleted = _category.DeleteCategory(id);
            if (!isDeleted)
                return NotFound();
            return Ok(isDeleted);

        }
    }
}
