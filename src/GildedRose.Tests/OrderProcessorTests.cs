using System;
using Xunit;

namespace GildedRose.Tests
{
    public class OrderProcessorTests
    {
        [Fact]
        public void YouCantOrderMoreThanWeHave()
        {
            var item = new InventoriedItem(
                Guid.NewGuid(),
                new Item(
                    "Batavian Colonial Silver Filigree Tray or Platter",
                    " Dutch East Indies, probably Batavia; and possibly India 17th-18th century; length: 42.7cm, width: 24.5cm, height: 3.7cm, weight: 957g",
                    5000
                ),
                1
            );
            var op = new OrderProcessor(
                Inventory.Build(new[] { item }),
                new PaymentProcessor()
            );

            var result = op.Place(new Order(
                new Customer(
                    Guid.NewGuid(),
                    "Example Customer"
                ),
                new[] {
                    new OrderItem(item.Id, 2)
                }
            ));

            Assert.IsType<CanceledOrder>(result);
        }
    }
}