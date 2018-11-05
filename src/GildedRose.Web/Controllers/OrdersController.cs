using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GildedRose.Web
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderProcessor orderProcessor;

        public OrdersController(IOrderProcessor orderProcessor)
        {
            this.orderProcessor = orderProcessor;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<OrderResult> PlaceOrder(Order order)
        {
            var result = orderProcessor.Place(order);
            switch (result)
            {
                case CanceledOrder co:
                    return BadRequest(co);
                default:
                    return result;
            }
        }
    }
}