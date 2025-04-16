using System;
using System.Collections;

namespace DataStructuresLab.Model
{
    public class DuplexLinkedList<T> : IEnumerable<T>
    {
        public Item<T>? Head { get; set; }
        public Item<T>? Tail { get; set; }
        public int Count { get; set; }

        public DuplexLinkedList() { }

        public DuplexLinkedList(T data)
        {
            var item = new Item<T>(data);
            Head = item;
            Tail = item;
            Count = 1;
        }

        public void Add(T data)
        {
            var item = new Item<T>(data);

            if (Count == 0)
            {
                Head = item;
                Tail = item;
                Count = 1;
                return;
            }

            Tail.Next = item;
            item.Previous = Tail;
            Tail = item;
            Count++;
        }

        public bool Delete(T data)
        {
            var current = Head;

            while (current != null)
            {
                if (current.Data != null && current.Data.Equals(data))
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

        public IEnumerator GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }
    }
}
