using System;
using System.Drawing;
using CarLibrary;
using DataStructuresLab.Model;

namespace DataStructuresLab.ModelHashSet
{
    public class HashTable<TKey, TValue>
    {
        private int defaultLength = 10;
        private Item<Entry<TKey, TValue>>[] set;

        public int Count { get => set.Length; }

        public bool isReadOnly => false;

        public HashTable()
        {
            set = new Item<Entry<TKey, TValue>>[defaultLength];
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не должен быть пустым.");

            int index = Math.Abs(key.GetHashCode() % Count);
            var newEntry = new Entry<TKey, TValue>(key, value);

            if (set[index] == null)
            {
                set[index] = new Item<Entry<TKey, TValue>>(newEntry);
            }
            else
            {
                Item<Entry<TKey, TValue>> current = set[index];
                while (current.Next != null)
                {
                    if (current.Data.Key.Equals(key))
                        return;
                    current = current.Next;
                }
                if (!current.Data.Key.Equals(key))
                    current.Next = new Item<Entry<TKey, TValue>>(newEntry);
            }
        }

        public void Clear()
        {
            set = new Item<Entry<TKey, TValue>>[defaultLength];
        }

        public bool Contains(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не может быть пустым.");

            int index = Math.Abs(key.GetHashCode() % set.Length);
            Item<Entry<TKey, TValue>> current = set[index];

            while (current != null)
            {
                if (current.Data.Key.Equals(key))
                    return true;
                current = current.Next;
            }

            return false;
        }

        public void CopyTo(Entry<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), "Массив не может быть пустым.");

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Индекс не может быть отрицательным.");

            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("Недостаточно места.", nameof(array));

            int currentIndex = arrayIndex;

            for (int i = 0; i < set.Length; i++)
            {
                Item<Entry<TKey, TValue>> current = set[i];

                while (current != null)
                {
                    array[currentIndex++] = new Entry<TKey, TValue>(current.Data.Key, current.Data.Value);
                    current = current.Next;
                }
            }
        }

        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не может быть пустым.");

            int index = Math.Abs(key.GetHashCode() % set.Length);
            Item<Entry<TKey, TValue>> current = set[index];

            if (current != null && current.Data.Key.Equals(key))
            {
                set[index] = current.Next;
                return true;
            }

            while (current != null && current.Next != null)
            {
                if (current.Next.Data.Equals(key))
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public void PrintHS()
        {
            if (set == null)
                throw new Exception("Таблица не создана.");

            for (int i = 0; i < set.Length; i++)
            {
                Console.WriteLine($"{i} :");
                if (set[i] != null)
                {
                    Item<Entry<TKey, TValue>> current = set[i];
                    while (current != null)
                    {
                        Console.WriteLine(current.Data);
                        current = current.Next;
                    }
                }
            }
        }
    }
}