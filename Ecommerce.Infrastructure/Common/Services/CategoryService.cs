using Ecommerce.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Common.DbContext;

namespace Ecommerce.Infrastructure.Common.Services
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddCategory(Domain.Models.Category category)
        {
            if (category == null)
                return false;

            try
            {
                _context.Categories.Add(category);
                 _context.SaveChanges();
                return true;
            }
            catch { 
                return false;
            }


        }
        public bool DeleteCategory(int id)
        {
            if (id <= 0)
                return false;

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Domain.Models.Category> GetAll()
        {
          var categories =  _context.Categories.ToList();
            if (categories.Any())
              return categories;

            return new List<Category>();
        }

        public Category GetCategoryById(int id)
        {
            
            if(id > 0)
            {
                var category = new Category();
                category = _context.Categories.Find(id);
                return category;
            }
            return new Category();
               
        }

        public Category UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
