using System;
using System.Collections;

namespace DataStructuresLab.Model
{
    public class DuplexLinkedList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Начало списка, не равный null
        /// </summary>
        public Item<T>? Head { get; private set; }
        /// <summary>
        /// Конец списка, не равный null
        /// </summary>
        public Item<T>? Tail { get; private set; }
        /// <summary>
        /// Количество элементов в списке
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Конуструктор без параметров
        /// </summary>
        public DuplexLinkedList() { }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="data">Параметр - данные элемента списка</param>
        public DuplexLinkedList(T data)
        {
            Add(data);
        }

        /// <summary>
        /// Добавляет элемент в конец списка
        /// </summary>
        /// <param name="data">Данные добавляемого элемента списка</param>
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

        /// <summary>
        /// Добавляет коллекцию элементов в конец списка
        /// </summary>
        /// <param name="items">Добавляемые элементы</param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Удаляет все элементы из списка
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        /// <summary>
        /// Метод, проверяющий является ли список пустым
        /// </summary>
        public bool IsEmpty => Count == 0;

        /// <summary>
        /// Проверяет, содержит ли список указанный элемент
        /// </summary>
        /// <param name="item">Искомый элемент</param>
        /// <returns>Возвращает булевое значение true/false в зависимости от того, найден ли элемент</returns>
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

        /// <summary>
        /// Удаляет первое вхождение указанного элемента из списка 
        /// </summary>
        /// <param name="data">Элемент для удаления</param>
        /// <returns>Возвращает булевое значение true/false в зависимости от того, найден ли элемент и удалён</returns>
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

        /// <summary>
        /// Метод, реверсирующий список
        /// </summary>
        public void Reverse()
        {
            var current = Head;
            Head = Tail;
            Tail = current;

            while (current != null)
            {
                var next = current.Next;
                current.Next = current.Previous;
                current.Previous = next;
                current = next;
            }
        }

        /// <summary>
        /// Метод, который выводит все элементы списка
        /// </summary>
        public void PrintLinkedList()
        {
            if (IsEmpty)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            foreach (var item in this)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Выполняет копирование списка в массив с указанного индекса
        /// </summary>
        /// <param name="array">Массив</param>
        /// <param name="arrayIndex">Индекс элемента массива</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (array.Length - arrayIndex < Count)
                throw new Exception("Недостаточно места.");
            var current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Data;
                current = current.Next;
            }
        }

        /// <summary>
        /// Удаляет все элементы, начиная с указанного до конца списка
        /// </summary>
        /// <param name="foundedItem">Элемент, от которого происходит удаление элементов</param>
        /// <returns>Возвращает количество удалённых элементов</returns>
        public int RemoveFromToEnd(T foundedItem)
        {
            int removedCount = 0;
            Item<T>? current = Head;

            while (current != null && !object.Equals(current.Data, foundedItem))
            {
                current = current.Next;
            }

            if (current != null)
            {
                Tail = current.Previous;

                if (current == Head)
                {
                    Head = null;
                }
                else
                {
                    current.Previous.Next = null;
                }

                while (current != null)
                {
                    removedCount++;
                    Count--;
                    current = current.Next;
                }
            }

            return removedCount;
        }

        /// <summary>
        /// Добавляет случайно сгенерированные элементы на нечётные позиции списка
        /// </summary>
        /// <param name="generator">Генератор новых элементов</param>
        /// <param name="count">Количество элементов, которые нужно вставить на нечётные индексы списка</param>
        public void AddAtOddPositions(Func<T> generator, int count)
        {
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            var itemsToAdd = new List<T>();
            for (int i = 0; i < count; i++)
            {
                itemsToAdd.Add(generator());
            }

            int targetPosition = 1;
            int currentPosition = 0;
            Item<T> current = Head;
            Item<T> previous = null;

            foreach (var item in itemsToAdd)
            {
                while (current != null && currentPosition < targetPosition)
                {
                    previous = current;
                    current = current.Next;
                    currentPosition++;
                }

                var newNode = new Item<T>(item);
                newNode.Previous = previous;
                newNode.Next = current;

                if (previous != null)
                    previous.Next = newNode;
                else
                    Head = newNode;

                if (current != null)
                    current.Previous = newNode;
                else
                    Tail = newNode;

                Count++;

                targetPosition += 2;
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
