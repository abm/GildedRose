using System;
using System.Linq;

namespace GildedRose
{
    public class OrderProcessor : IOrderProcessor
    {
        private readonly IInventory inventory;
        private readonly IPaymentProcessor payments;

        public OrderProcessor(IInventory inventory, IPaymentProcessor payments)
        {
            this.inventory = inventory;
            this.payments = payments;
        }

        public OrderResult Place(Order order)
        {
            var outOfStock = order.Items
                .Where(item => inventory.RemainingStock(item.ItemId) == 0)
                .Select(item => inventory.Items.Single(i => i.Id == item.ItemId).Item.Name);

            if (outOfStock.Any())
            {
                return new CanceledOrder(
                    Guid.NewGuid(),
                    order,
                    $"We're out of the following: {string.Join(", ", outOfStock)}");
            }

            var notEnoughStock = order.Items
                .Where(item => inventory.RemainingStock(item.ItemId) < item.Count)
                .Select(item => inventory.Items.Single(i => i.Id == item.ItemId).Item.Name);

            if (notEnoughStock.Any())
            {
                return new CanceledOrder(
                    Guid.NewGuid(),
                    order,
                    $"We're don't have enough of the following: {string.Join(", ", notEnoughStock)}");
            }


            var paymentResult = payments.PayFor(order);
            switch (paymentResult)
            {
                case PaymentResult.Successful:
                    foreach (var item in order.Items)
                        inventory.ReduceStock(item.ItemId, item.Count);
                    return new FulfilledOrder(Guid.NewGuid(), order);
                case PaymentResult.OutOfFunds:
                    return new CanceledOrder(Guid.NewGuid(), order, "Insufficient funds");
                case PaymentResult.NoPaymentMethod:
                    return new CanceledOrder(Guid.NewGuid(), order, "No payment method set");
                default:
                    return new CanceledOrder(Guid.NewGuid(), order, $"We don't know how to handle {paymentResult}");
            }
        }
    }
}