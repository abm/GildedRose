using System;
using Xunit;
using Xunit.Abstractions;
using FsCheck;
using FsCheck.Xunit;

namespace GildedRose.Tests
{
    public class ItemTests
    {
        private readonly ITestOutputHelper outputHelper;

        public ItemTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Property]
        public Property ItemsWithTheSameValuesEqualEachOther(
            string description, string name, int price
        )
        {
            var a = new Item(name, description, price);
            var b = new Item(name, description, price);

            return (a == b).ToProperty();
        }

        [Property]
        public Property ItemsWithTheDifferentValuesDoNotEqualEachOther(
            string x, string y, int price
        )
        {
            var a = new Item(x, y, price);
            var b = new Item(y, x, price);

            Func<bool> property = () => a != b;
            return property.When(x != y);
        }
    }
}