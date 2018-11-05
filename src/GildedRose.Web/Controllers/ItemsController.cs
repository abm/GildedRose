using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace GildedRose.Web
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IInventory inventory;

        public ItemsController(IInventory inventory)
        {
            this.inventory = inventory;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InventoriedItem>> GetItems()
        {
            return Ok(inventory.Items);
        }
    }
}