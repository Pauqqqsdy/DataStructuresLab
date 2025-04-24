using System;
using CarLibrary;
using DataStructuresLab.ModelHashSet;

namespace DataStructuresLab
{
    public class TransportsHashTableActions
    {
        private HashTable<int, Transport> transportsHashTable;
        private Random rnd = new Random();

        public TransportsHashTableActions(HashTable<int, Transport> hashTable)
        {
            transportsHashTable = hashTable;
        }

        /// <summary>
        /// Создание и заполнение хеш-таблицы
        /// </summary>
        public void CreateAndFillHashTable()
        {
            transportsHashTable.Clear();

            Console.Write("Введите количество элементов: ");
            int count = int.Parse(Console.ReadLine()!);

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nСоздание элемента {i + 1}:");
                Console.WriteLine("1. Транспорт");
                Console.WriteLine("2. Грузовик");
                Console.WriteLine("3. Внедорожник");
                Console.WriteLine("4. Легковой автомобиль");
                Console.Write("Выберите тип: ");

                int type = int.Parse(Console.ReadLine()!);

                Transport transport;
                switch (type)
                {
                    case 1:
                        transport = new Transport();
                        transport.RandomCreate();
                        break;

                    case 2:
                        transport = new Truck();
                        transport.RandomCreate();
                        break;

                    case 3:
                        transport = new OffroadCar();
                        transport.RandomCreate();
                        break;

                    case 4:
                        transport = new PassengerCar();
                        transport.RandomCreate();
                        break;

                    default:
                        Console.WriteLine("Неверный выбор. Создан обычный транспорт по умолчанию.");
                        transport = new Transport();
                        break;
                }

                transportsHashTable.Add(transport.ID.Id, transport);
            }

            Console.WriteLine($"Хеш-таблица создана и заполнена {count} элементами.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Вывод всей хеш-таблицы
        /// </summary>
        public void PrintHashTable()
        {
            if (transportsHashTable.Count == 0)
            {
                Console.WriteLine("Хеш-таблица пуста.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("Элементы хеш-таблицы:");
            transportsHashTable.PrintHS();

            Console.WriteLine("\nОбщее количество элементов: " + transportsHashTable.Count);
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Удаление транспорта по ID
        /// </summary>
        public void RemoveTransport()
        {
            if (transportsHashTable.Count == 0)
            {
                Console.WriteLine("Хеш-таблица пуста. Нечего удалять.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.Write("Введите ID транспорта для удаления: ");
            int id = int.Parse(Console.ReadLine()!);

            if (transportsHashTable.Remove(id))
            {
                Console.WriteLine($"Транспорт с ID {id} успешно удалён.");
            }
            else
            {
                Console.WriteLine($"Транспорт с ID {id} не найден.");
            }

            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Поиск транспорта по ID
        /// </summary>
        public void FindTransport()
        {
            if (transportsHashTable.Count == 0)
            {
                Console.WriteLine("Хеш-таблица пуста.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.Write("Введите ID транспорта для поиска: ");
            int id = int.Parse(Console.ReadLine()!);

            try
            {
                var transport = transportsHashTable.Get(id);
                Console.WriteLine($"Найден транспорт: {transport}");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"Транспорт с ID {id} не найден.");
            }

            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Добавление случайных транспортов
        /// </summary>
        public void AddRandomTransports()
        {
            Console.Write("Введите количество элементов для добавления: ");
            int count = int.Parse(Console.ReadLine()!);

            for (int i = 0; i < count; i++)
            {
                Transport transport;
                int type = rnd.Next(1, 5);

                switch (type)
                {
                    case 1:
                        transport = new Transport();
                        transport.RandomCreate();
                        break;

                    case 2:
                        transport = new Truck();
                        transport.RandomCreate();
                        break;

                    case 3:
                        transport = new OffroadCar();
                        transport.RandomCreate();
                        break;

                    case 4:
                        transport = new PassengerCar();
                        transport.RandomCreate();
                        break;

                    default:
                        transport = new Transport();
                        break;
                }

                transportsHashTable.Add(transport.ID.Id, transport);
            }

            Console.WriteLine($"Добавлено {count} транспортов.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Клонирование хеш-таблицы
        /// </summary>
        public void CopyHashTable()
        {
            if (transportsHashTable.Count == 0)
            {
                Console.WriteLine("Хеш-таблица пуста. Копирование невозможно.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Entry<int, Transport>[] entries = new Entry<int, Transport>[transportsHashTable.Count];

            transportsHashTable.CopyTo(entries, 0);

            HashTable<int, Transport> copiedTable = new HashTable<int, Transport>();
            foreach (var entry in entries)
            {
                copiedTable.Add(entry.Key, entry.Value);
            }

            Console.WriteLine("\nНачальная хеш-таблица");
            transportsHashTable.PrintHS();

            Console.WriteLine("\nСкопированная хеш-таблица");
            copiedTable.PrintHS();

            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Очистка хеш-таблицы
        /// </summary>
        public void ClearHashTable()
        {
            transportsHashTable.Clear();
            Console.WriteLine("Хеш-таблица очищена.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
