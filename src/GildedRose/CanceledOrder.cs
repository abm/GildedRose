using System;

namespace GildedRose
{
    public class CanceledOrder : OrderResult
    {
        public string Reason { get; }

        public CanceledOrder(Guid id, Order order, string reason)
            : base(id, order) => Reason = reason;
    }
}