using System;

namespace GildedRose
{
    public class Customer
    {
        public Guid Id { get; }
        public string Name { get; }

        public Customer(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}