using System;

namespace GildedRose
{
    public class Item
    {
        public string Description { get; }
        public string Name { get; }
        public int Price { get; } // int due to high prices; no need for cents

        public Item(string name, string description, int price)
        {
            Description = description;
            Name = name;
            Price = price;
        }
    }
}
