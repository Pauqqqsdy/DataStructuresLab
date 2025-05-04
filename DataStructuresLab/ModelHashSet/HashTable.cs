using System;
using System.Collections;
using CarLibrary;
using DataStructuresLab.Model;

namespace DataStructuresLab.ModelHashSet
{
    public class HashTable<TKey, TValue> : IEnumerable<Entry<TKey, TValue>>
    {
        /// <summary>
        /// Начальный размер таблицы = 10
        /// </summary>
        private int defaultSize = 10;

        /// <summary>
        /// Коэффициент заполнения для расширения
        /// </summary>
        private double loadFactor = 0.75;

        /// <summary>
        /// Предельное количество элементов перед расширением хеш-таблицы
        /// </summary>
        private int limitFactor;

        /// <summary>
        /// Массив цепочек
        /// </summary>
        private ItemTable<Entry<TKey, TValue>>[] set;

        /// <summary>
        /// Текущее кол-во элементов в таблице
        /// </summary>
        private int count = 0;

        /// <summary>
        /// Метод, возвращающий длину цепочки
        /// </summary>
        /// <returns></returns>
        internal int GetArrayLength() => set.Length;

        /// <summary>
        /// Метод, возвращающий кол-во элементов
        /// </summary>
        public int Count => count;

        public bool isReadOnly => false;

        /// <summary>
        /// Таблица с начальным размером
        /// </summary>
        public HashTable()
        {
            set = new ItemTable<Entry<TKey, TValue>>[defaultSize];
            limitFactor = (int)(defaultSize * loadFactor);
        }

        /// <summary>
        /// Метод, добавляющий новый элемент по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
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
                set[index] = new ItemTable<Entry<TKey, TValue>>(newEntry);
                count++;
            }
            else
            {
                ItemTable<Entry<TKey, TValue>> current = set[index];
                while (current.Next != null)
                {
                    if (current.Data.Key.Equals(key))
                        throw new ArgumentException(nameof(key), "Элемент с таким ключом уже существует.");
                    current = current.Next;
                }
                if (!current.Data.Key.Equals(key))
                {
                    current.Next = new ItemTable<Entry<TKey, TValue>>(newEntry);
                    count++;
                }
             }
        }

        /// <summary>
        /// Увеличивает таблицу в 2 раза
        /// </summary>
        private void Resize()
        {
            int newSize = set.Length * 2;
            var newTable = new ItemTable<Entry<TKey, TValue>>[newSize];

            foreach (var entry in this)
            {
                int newIndex = Math.Abs(entry.Key.GetHashCode() % newSize);
                if (newTable[newIndex] == null)
                {
                    newTable[newIndex] = new ItemTable<Entry<TKey, TValue>>(entry);
                }
                else
                {
                    ItemTable<Entry<TKey, TValue>> current = newTable[newIndex];
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = new ItemTable<Entry<TKey, TValue>>(entry);
                }
            }

            set = newTable;
            limitFactor = (int)(newSize * loadFactor);
        }

        /// <summary>
        /// Очищает таблицу до начального состояния
        /// </summary>
        public void Clear()
        {
            set = new ItemTable<Entry<TKey, TValue>>[defaultSize];
            count = 0;
        }

        /// <summary>
        /// Проверяет наличие элемента по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Возвращает false, если элемента нет в таблице и true, если он есть</returns>
        public bool Contains(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не может быть пустым.");

            int index = Math.Abs(key.GetHashCode() % set.Length);
            ItemTable<Entry<TKey, TValue>> current = set[index];

            while (current != null)
            {
                if (current.Data.Key.Equals(key))
                    return true;
                current = current.Next;
            }

            return false;
        }

        /// <summary>
        /// Копирует элементы в массив
        /// </summary>
        /// <param name="array">Массив</param>
        /// <param name="arrayIndex">Индекс</param>
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
                ItemTable<Entry<TKey, TValue>> current = set[i];

                while (current != null)
                {
                    array[currentIndex++] = new Entry<TKey, TValue>(current.Data.Key, current.Data.Value);
                    current = current.Next;
                }
            }
        }

        /// <summary>
        /// Удаляет элемент по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Возвращает true, если элемент был найден и удалён и false, если элемент не был найден</returns>
        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не может быть пустым.");

            int index = Math.Abs(key.GetHashCode() % set.Length);
            ItemTable<Entry<TKey, TValue>> current = set[index];

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

        /// <summary>
        /// Возвращает значение элемента по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        public TValue Get(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не может быть пустым.");

            int index = Math.Abs(key.GetHashCode() % set.Length);
            ItemTable<Entry<TKey, TValue>> current = set[index];

            while (current != null)
            {
                if (current.Data.Key.Equals(key))
                    return current.Data.Value;
                current = current.Next;
            }

            throw new KeyNotFoundException("Ключ не найден.");
        }

        /// <summary>
        /// Выводит таблицу
        /// </summary>
        public void PrintHS()
        {
            if (set == null)
                throw new Exception("Таблица не создана.");

            Console.WriteLine("Содержимое хеш-таблицы:");
            int totalElements = 0;

            for (int i = 0; i < set.Length; i++)
            {
                if (set[i] != null)
                {
                    Console.WriteLine($"[{i}]: ");

                    ItemTable<Entry<TKey, TValue>> current = set[i];
                    int chainLength = 0;

                    while (current != null)
                    {
                        Console.WriteLine($"Ключ: {current.Data.Key}, Значение: {current.Data.Value}");
                        current = current.Next;
                        chainLength++;
                        totalElements++;
                    }
                }
            }

            Console.WriteLine($"Всего элементов: {totalElements}");
            Console.WriteLine($"Коэффициент заполнения: {(double)count / set.Length:P}");
        }

        public IEnumerator<Entry<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < set.Length; i++)
            {
                ItemTable<Entry<TKey, TValue>> current = set[i];
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