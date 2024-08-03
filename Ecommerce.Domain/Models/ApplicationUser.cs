using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Wallet? Wallet { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
