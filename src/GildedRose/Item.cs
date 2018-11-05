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

        public override bool Equals(object obj)
        {
            return Equals(obj as Item);
        }

        public bool Equals(Item other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Description == other.Description && Name == other.Name && Price == other.Price;
        }

        public static bool operator ==(Item a, Item b)
        {
            if (Object.ReferenceEquals(a, null))
            {
                if (Object.ReferenceEquals(b, null))
                    return true;
                return false;
            }
            return a.Equals(b);
        }

        public static bool operator !=(Item a, Item b) => !(a == b);

        public override int GetHashCode() => (Description, Name, Price).GetHashCode();

        public override string ToString() => $"{Name} @ ${Price}: {Description}";
    }
}
