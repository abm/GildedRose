using System;
using System.Collections.Generic;

namespace GildedRose
{
    public interface IInventory
    {
        IEnumerable<InventoriedItem> Items { get; }
        InventoriedItem Add(InventoriedItem item);
        InventoriedItem AddStock(Guid itemId, int count);
        InventoriedItem ReduceStock(Guid itemId, int count);
        int RemainingStock(Guid itemId);
    }
}