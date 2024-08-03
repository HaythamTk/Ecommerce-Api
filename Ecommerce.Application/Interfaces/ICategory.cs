using Ecommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    public interface ICategory
    {
        public bool AddCategory(Category category);
        public Category UpdateCategory(Category category);
        public bool DeleteCategory(int id);
        public IEnumerable<Category> GetAll();
        public Category GetCategoryById(int id);
    }
}
