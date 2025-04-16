using System;
using System.Collections;

namespace DataStructuresLab.Model
{
    public class DuplexLinkedList<T> : IEnumerable<T>
    {
        public Item<T>? Head { get; private set; }
        public Item<T>? Tail { get; private set; }
        public int Count { get; private set; }

        public DuplexLinkedList() { }

        public DuplexLinkedList(T data)
        {
            Add(data);
        }

        public void Add(T data)
        {
            var item = new Item<T>(data);

            if (Count == 0)
            {
                Head = item;
                Tail = item;
            }
            else
            {
                Tail.Next = item;
                item.Previous = Tail;
                Tail = item;
            }
            Count++;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            var current = Head;
            while (current != null)
            {
                if ((item == null && current.Data == null) ||
                    (item != null && item.Equals(current.Data)))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public bool Remove(T data)
        {
            var current = Head;

            while (current != null)
            {
                if (object.Equals(current.Data, data))
                {
                    if (current.Previous != null)
                        current.Previous.Next = current.Next;
                    else
                        Head = current.Next;

                    if (current.Next != null)
                        current.Next.Previous = current.Previous;
                    else
                        Tail = current.Previous;

                    Count--;
                    return true;
                }

                current = current.Next;
            }
            return false;
        }

        public DuplexLinkedList<T> Reverse()
        {
            var result = new DuplexLinkedList<T>();

            var current = Tail;

            while(current != null)
            {
                result.Add(current.Data);
                current = current.Previous;
            }
            return result;
        }

        public void PrintAll()
        {
            if (Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            int index = 1;
            Item<T>? current = Head;
            while (current != null)
            {
                Console.WriteLine($"{index}. {current.Data}");
                current = current.Next;
                index++;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new Exception("Недостаточно места в массиве.");
            var current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Data;
                current = current.Next;
            }
        }

        public IEnumerator GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }
    }
}
