using System;
using System.Collections.Generic;

namespace GildedRose
{
    public class Inventory : IInventory
    {
        private Dictionary<Guid, InventoriedItem> db;

        public Inventory()
        {
            db = new Dictionary<Guid, InventoriedItem>();
        }

        public IEnumerable<InventoriedItem> Items => db.Values;

        public static Inventory Build(IEnumerable<InventoriedItem> items)
        {
            var inventory = new Inventory();
            foreach (var item in items)
                inventory.Add(item);
            return inventory;
        }

        public InventoriedItem Add(InventoriedItem item)
        {
            if (!db.ContainsKey(item.Id))
            {
                db.Add(item.Id, item);
                return item;
            }

            // Let's allow for repeats, e.g., a messy spreadsheet,
            // so we'll increase stock counts instead of overwriting.
            db[item.Id].AddStock(item.Count);
            return item;
        }

        public InventoriedItem AddStock(Guid itemId, int count)
        {
            if (!db.ContainsKey(itemId))
                throw new ArgumentException($"We have no record of {itemId}");

            var item = db[itemId];
            item.AddStock(count);
            return item;
        }

        public int RemainingStock(Guid itemId)
        {
            if (!db.ContainsKey(itemId))
                throw new ArgumentException($"We have no record of {itemId}");

            return db[itemId].Count;
        }
    }
}