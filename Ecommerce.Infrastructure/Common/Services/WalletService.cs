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
    public class WalletService : IWallet
    {
        private readonly ApplicationDbContext _context;
        public WalletService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddWallet(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;
            try
            {
                var wallet = new Wallet
                {
                    Balance = 0,
                    UserId = userId,
                };
                _context.Wallets.Add(wallet);
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
