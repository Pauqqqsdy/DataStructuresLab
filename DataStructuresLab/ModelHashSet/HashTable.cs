using System;
using System.Collections;
using CarLibrary;
using DataStructuresLab.Model;

namespace DataStructuresLab.ModelHashSet
{
    public class HashTable<TKey, TValue> : IEnumerable<Entry<TKey, TValue>>
    {
        private int defaultSize = 10;
        private double loadFactor = 0.75;
        private int limitFactor;
        private Item<Entry<TKey, TValue>>[] set;
        private int count = 0;

        public int Count => count;

        public bool isReadOnly => false;

        public HashTable()
        {
            set = new Item<Entry<TKey, TValue>>[defaultSize];
            limitFactor = (int)(defaultSize * loadFactor);
        }

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не должен быть пустым.");
            
            if (count >= limitFactor)
            {
                Resize();
            }

            int index = Math.Abs(key.GetHashCode() % set.Length);
            var newEntry = new Entry<TKey, TValue>(key, value);

            if (set[index] == null)
            {
                set[index] = new Item<Entry<TKey, TValue>>(newEntry);
                count++;
            }
            else
            {
                Item<Entry<TKey, TValue>> current = set[index];
                while (current.Next != null)
                {
                    if (current.Data.Key.Equals(key))
                        throw new ArgumentException(nameof(key), "Элемент с таким ключом уже существует.");
                    current = current.Next;
                }
                if (!current.Data.Key.Equals(key))
                {
                    current.Next = new Item<Entry<TKey, TValue>>(newEntry);
                    count++;
                }
             }
        }

        private void Resize()
        {
            int newSize = set.Length * 2;
            var newTable = new Item<Entry<TKey, TValue>>[newSize];

            foreach (var entry in this)
            {
                int newIndex = Math.Abs(entry.Key.GetHashCode() % newSize);
                if (newTable[newIndex] == null)
                {
                    newTable[newIndex] = new Item<Entry<TKey, TValue>>(entry);
                }
                else
                {
                    Item<Entry<TKey, TValue>> current = newTable[newIndex];
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = new Item<Entry<TKey, TValue>>(entry);
                }
            }

            set = newTable;
            limitFactor = (int)(newSize * loadFactor);
        }

        public void Clear()
        {
            set = new Item<Entry<TKey, TValue>>[defaultSize];
            count = 0;
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
                count--;
                return true;
            }

            while (current != null && current.Next != null)
            {
                if (current.Next.Data.Key.Equals(key))
                {
                    current.Next = current.Next.Next;
                    count--;
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public TValue Get(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не может быть пустым.");

            int index = Math.Abs(key.GetHashCode() % set.Length);
            Item<Entry<TKey, TValue>> current = set[index];

            while (current != null)
            {
                if (current.Data.Key.Equals(key))
                    return current.Data.Value;
                current = current.Next;
            }

            throw new KeyNotFoundException("Ключ не найден.");
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

        public IEnumerator<Entry<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < set.Length; i++)
            {
                Item<Entry<TKey, TValue>> current = set[i];
                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}