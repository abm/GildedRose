using System;

namespace GildedRose
{
    public class OrderItem
    {
        public Guid ItemId { get; }
        public int Count { get; }

        public OrderItem(Guid itemId, int count)
        {
            Count = count;
            ItemId = itemId;
        }
    }
}