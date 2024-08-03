using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
     public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<ApplicationUser>()
            //    .HasOne(a => a.Wallet)
            //    .WithOne(b => b.User)
            //    .HasForeignKey<Wallet>(a => a.UserId)
            //    .IsRequired();
            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}
