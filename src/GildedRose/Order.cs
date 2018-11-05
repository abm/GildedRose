using System.Collections.Generic;

namespace GildedRose
{
    public class Order
    {
        public Customer Customer { get; }
        public IEnumerable<OrderItem> Items { get; }

        public Order(Customer customer, IEnumerable<OrderItem> items)
        {
            Customer = customer;
            Items = items;
        }
    }
}