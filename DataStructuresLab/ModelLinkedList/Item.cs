using System;

namespace DataStructuresLab.Model
{
    public class Item<T>
    {
        /// <summary>
        /// Данные элемента списка
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Ссылка на предыдущий элемент
        /// </summary>
        public Item<T>? Previous { get; set; }

        /// <summary>
        /// Ссылка на следующий элемент
        /// </summary>
        public Item<T>? Next { get; set; }

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public Item() { }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="data">Параметр - данные элемента списка</param>
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
