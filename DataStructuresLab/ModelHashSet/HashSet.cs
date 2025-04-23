using System;
using CarLibrary;
using DataStructuresLab.Model;

namespace DataStructuresLab.ModelHashSet
{
    public class HashSet<T>
    {
        int defaultLength = 10;
        Point<T>[] set;
        public int Count { get => set.Length; }

        public bool isReadOnly => false;

        public HashSet()
        {
            set = new Point<T>[defaultLength];
        }

        public void Add(T item)
        {
            if (item == null)
                throw new Exception("Ссылка для добавления пустая.");
            int index = Math.Abs(item.GetHashCode() % Count);
            if (set[index] == null)
            {
                set[index] = new Point<T>(item);
            }
            else
            {
                Point<T> current = set[index];
                while (current.Next != null)
                {
                    if (current.Data.Equals(item))
                        return;
                    current = current.Next;
                }
                if (!current.Data.Equals(item))
                    current.Next = new Point<T>(item);
            }
        }

        public void Clear()
        {
            set = new Point<T>[defaultLength];
        }

        public bool Contains(T item)
        {
            int index = Math.Abs(item.GetHashCode() % Count);
            if (set[index] == null)
            {
                return false;
            }

            Point<T> current = set[index];
            if (current.Data.Equals(item))
                return true;

            if (current.Next == null)
                return false;

            while (current != null)
            {
                if (current.Data.Equals(item))
                    return true;
                current = current.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        { 
        }

        public bool Remove(T item)
        {
            int index = Math.Abs(item.GetHashCode() % Count);

            if (set[index] == null)
            {
                return false;
            }

            Point<T> current = set[index];
            if (set[index].Data.Equals(item))
            {
                if (current.Next == null)
                {
                    set[index] = null;
                    return true;
                }
                else
                {
                    set[index] = current.Next;
                    return true;
                }
            }
            
            while (current.Next != null)
            {
                if (current.Next.Data.Equals(item))
                {
                    current.Next = current.Next.Next;
                    return true;
                }
                current = current.Next;
            }

            if (current.Next.Data.Equals(item))
            {
                current.Next = null;
                return true;
            }

            return false;
        }

        public void PrintHS()
        {
            if (set == null)
                throw new Exception("Таблица не создана.");

            for (int i = 0; i < set.Length; i++)
            {
                Console.WriteLine($" {i} :");
                if (set[i] != null)
                {
                    Point<T> current = set[i];
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