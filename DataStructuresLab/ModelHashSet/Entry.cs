using System;

namespace DataStructuresLab.ModelHashSet
{
    public class Entry<TKey, TValue>
    {
        /// <summary>
        /// Ключ элемента
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Значение элемента
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Конструктор, инициализирующий ключ и значение
        /// </summary>
        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Метод, возвращающий строковое представление в формате "Ключ: Значение"
        /// </summary>
        public override string ToString()
        {
            return $"{Key}: {Value}";
        }
    }
}
