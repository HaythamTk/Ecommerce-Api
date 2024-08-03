using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Common.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddProduct(Product product)
        {
            if (product == null)
                return false;
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            if (id <= 0)
                return false;

            var product = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (product != null)
            {
                _context.Categories.Remove(product);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Product> GetAll()
        {
            var products = _context.Products.Include(x=>x.Category).ToList();
            if (products.Any())
                return products;

            return new List<Product>();
        }

        public Product GetProductById(int id)
        {
            if (id > 0)
            {
                var product = new Product();
                product = _context.Products.Find(id);
                return product;
            }
            return new Product();
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
