using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace GildedRose.Web
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return new[] {
                new Item("Test", "Test", 10)
            };
        }
    }
}