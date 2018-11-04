using System;

namespace GildedRose
{
    public class InventoriedItem
    {
        public int Count { get; private set; }
        public Guid Id { get; }
        public Item Item { get; }

        public InventoriedItem(Guid id, Item item, int count)
        {
            Count = count;
            Id = id;
            Item = item;
        }

        public int AddStock(int count)
        {
            // Treat counts < 0 as mistakes, not errors
            if (count > 0) Count += count;
            return Count;
        }
    }
}