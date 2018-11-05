using System;

namespace GildedRose
{
    public class FulfilledOrder : OrderResult
    {
        public FulfilledOrder(Guid id, Order order)
            : base(id, order) { }
    }
}