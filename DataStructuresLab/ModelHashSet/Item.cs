using System;

namespace DataStructuresLab.ModelHashSet
{
    class Item<T>
    {
        public T? Data { get; set; }

        public Item<T>? Next { get; set; }

        public Item()
        {
            Data = default(T);
            Next = null;
        }

        public Item(T? data)
        {
            Data = data;
            Next = null;
        }

        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }

        public override int GetHashCode()
        {
            return Data?.GetHashCode() ?? 0;
        }
    }
}
