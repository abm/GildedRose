using System;

namespace GildedRose
{
    public abstract class OrderResult
    {
        public Guid Id { get; }
        public Order Order { get; }

        public OrderResult(Guid id, Order order)
        {
            Id = id;
            Order = order;
        }
    }
}