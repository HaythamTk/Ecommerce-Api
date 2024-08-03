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
    public class OrderItemService : IOrderItem
    {
        private readonly ApplicationDbContext _context;
        public OrderItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
                return false;

            try
            {
                _context.OrderItems.Add(orderItem);
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
