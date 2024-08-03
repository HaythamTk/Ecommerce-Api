using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainOperations : ControllerBase
    {
        private readonly IOrder _order;
        private readonly IOrderItem _orderItem;
        private readonly ICheckoutService _checkoutService;

        public MainOperations(IOrder order, IOrderItem orderItem, ICheckoutService checkoutService)
        {
            _order = order;
            _orderItem = orderItem;
            _checkoutService = checkoutService;
        }

        [HttpPost("Checkout")]
        public ActionResult Checkout(int shoppingCart)
        {
            if (shoppingCart <=0)
                return BadRequest("shoppingCart is empty");
            var isCheckout =  _checkoutService.ProcessCheckout(shoppingCart);
            if (!isCheckout)
                return BadRequest("something not right");
            return Ok("checkour sucess");
        }



    }
}
