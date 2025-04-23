using System;

namespace DataStructuresLab.ModelHashSet
{
    public class ItemTable<T>
    {
        public T? Data { get; set; }

        public ItemTable<T>? Next { get; set; }

        public ItemTable()
        {
            Data = default(T);
            Next = null;
        }

        public ItemTable(T? data)
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
