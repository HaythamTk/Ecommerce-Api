using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Common.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Services
{
    public class OrderService : IOrder
    {
        private readonly ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateOrder(Order order)
        {
            if (order == null)
                return false;

            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
