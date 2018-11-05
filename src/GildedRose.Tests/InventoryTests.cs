using System;
using Xunit;
using FsCheck;
using FsCheck.Xunit;

namespace GildedRose.Tests
{
    public class InventoryTests
    {
        [Fact]
        public void TrivialAddingStock()
        {
            var inventory = new Inventory();
            var initialStock = 1;
            var addedStock = 3;
            var item = new InventoriedItem(
                Guid.NewGuid(),
                new Item(
                    "Armenian Enamelled & Filigree Silver Holy Altar Box",
                    "Armenia/Ottoman Turkey 18th-19th century; length: 7cm, width: 5.3cm, height: 2.6cm, weight: 61.45g",
                    10000
                ),
                initialStock
            );
            inventory.Add(item);

            inventory.AddStock(item.Id, addedStock);

            Assert.Equal(initialStock + addedStock, inventory.RemainingStock(item.Id));
        }

        [Property]
        public Property AddingStock(int count)
        {
            var inventory = new Inventory();
            var initialStock = 1;
            var item = new InventoriedItem(
                Guid.NewGuid(),
                new Item(
                    "Armenian Enamelled & Filigree Silver Holy Altar Box",
                    "Armenia/Ottoman Turkey 18th-19th century; length: 7cm, width: 5.3cm, height: 2.6cm, weight: 61.45g",
                    10000
                ),
                initialStock
            );
            inventory.Add(item);

            inventory.AddStock(item.Id, count);

            var expected = count < 0 ? initialStock : initialStock + count;
            return (inventory.RemainingStock(item.Id) == expected).ToProperty();
        }

        [Property]
        public Property StockIsNeverLessThanZero(int count)
        {
            var inventory = new Inventory();
            var initialStock = 1;
            var item = new InventoriedItem(
                Guid.NewGuid(),
                new Item(
                    "Armenian Enamelled & Filigree Silver Holy Altar Box",
                    "Armenia/Ottoman Turkey 18th-19th century; length: 7cm, width: 5.3cm, height: 2.6cm, weight: 61.45g",
                    10000
                ),
                initialStock
            );
            inventory.Add(item);

            inventory.AddStock(item.Id, count);

            return (inventory.RemainingStock(item.Id) > 0).ToProperty();
        }
    }
}
