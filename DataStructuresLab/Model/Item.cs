using System;

namespace DataStructuresLab.Model
{
    public class Item<T>
    {
        public T? Data { get; set; }
        public Item<T>? Previous { get; set; }
        public Item<T>? Next { get; set; }

        public Item() { }

        public Item(T data)
        {
            Data = data;
        }

        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }
    }
}
