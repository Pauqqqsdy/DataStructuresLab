using System;

namespace DataStructuresLab.ModelHashSet
{
    public class ItemTable<T>
    {
        /// <summary>
        /// Данные
        /// </summary>
        public T? Data { get; set; }


        public ItemTable<T>? Next { get; set; }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ItemTable()
        {
            Data = default(T);
            Next = null;
        }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        public ItemTable(T? data)
        {
            Data = data;
            Next = null;
        }

        /// <summary>
        /// Метод, возвращающий строковое состояние
        /// </summary>
        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }

        /// <summary>
        /// Возвращает хеш-код объекта
        /// </summary>
        public override int GetHashCode()
        {
            return Data?.GetHashCode() ?? 0;
        }
    }
}
