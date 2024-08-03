using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Common.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IShoppingCart _shoppingCart;
        private readonly IWallet _wallet;
        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IShoppingCart shoppingCart, IWallet wallet)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _shoppingCart = shoppingCart;
            _wallet = wallet;
        }
        [HttpPost("SignUp")]
        public async Task <IActionResult> SignUp()
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = "User3",
                    Email = "User3@gmail.com",
                    //PasswordHash = "@User12345678"
                };
                var password = "@User12345678"; 
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = passwordHasher.HashPassword(user, password);
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest("User exists");
                }
                await _userManager.CreateAsync(user);
               // _context.Users.Add(user);
                var isShoppingCartAdd = _shoppingCart.AddShoppingCart(user.Id);
                var isWalletAdd = _wallet.AddWallet(user.Id);
                if(isShoppingCartAdd && isWalletAdd)
                {
                    _context.SaveChanges();
                    return Ok();
                }
               return BadRequest("Something Not Right");
            }
             catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllUser")]
        public IActionResult GetAllUser()
        {
            var users = _context.Users
                .Include(x => x.Wallet)
                .Include(x => x.ShoppingCart)
                .ThenInclude(x => x.Cart)
                .ThenInclude(x=> x.Product)
                .Select(x => new
                {
                    x.UserName,
                    x.Id,
                    UserBalance = x.Wallet != null ? x.Wallet.Balance : (decimal?)null,
                    CartItems = x.ShoppingCart != null ? x.ShoppingCart.Cart.Select(cartItem => new
                    {
                        ProductId = cartItem.ProductId,
                        productName = cartItem.Product.Name,
                        productPrice = cartItem.Product.Price,
                        ProductQuantity = cartItem.ProductQuantity,

                    }).ToList<object>() : new List<object>()
                })
                .ToList();

            return Ok(users);
        }

       
    }
}
