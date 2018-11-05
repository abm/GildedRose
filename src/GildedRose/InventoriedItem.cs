using System;

namespace GildedRose
{
    public class InventoriedItem
    {
        public int Count { get; private set; }
        public Guid Id { get; }
        public Item Item { get; }

        public InventoriedItem(Guid id, Item item, int count)
        {
            Count = count;
            Id = id;
            Item = item;
        }

        public int AddStock(int count)
        {
            // Treat counts < 0 as mistakes, not errors
            if (count > 0) Count += count;
            return Count;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as InventoriedItem);
        }

        public bool Equals(InventoriedItem other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;

            if (Object.ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id == other.Id && Item == other.Item && Count == other.Count;
        }

        public override int GetHashCode()
        {
            return (Count, Id, Item).GetHashCode();
        }
    }
}