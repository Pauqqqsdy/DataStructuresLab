using CarLibrary;
using DataStructuresLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLab
{
    public class TransportsListActions
    {
        private DuplexLinkedList<Transport> transportList;
        private Random rnd = new Random();

        /// <summary>
        /// Список транспортов при взаимодействии с меню
        /// </summary>
        /// <param name="list">Список транспортов</param>
        public TransportsListActions(DuplexLinkedList<Transport> list)
        {
            transportList = list;
        }

        /// <summary>
        /// Метод, создающий список и заполняющий его
        /// При этом предварительно очищает предыдущий список
        /// </summary>
        public void CreateAndFillList()
        {
            transportList.Clear();

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

                transportList.Add(transport);
            }

            Console.WriteLine($"Список создан.");

            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Метод, позволяющий вывести весь список
        /// </summary>
        public void PrintList()
        {
            if (transportList.IsEmpty)
            {
                Console.WriteLine("Список пуст.");

                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("Весь список:");
            transportList.PrintLinkedList();
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Метод удаления от заданного элемента включительно до конца списка
        /// </summary>
        public void DeleteFromToEnd()
        {
            if (transportList.IsEmpty)
            {
                Console.WriteLine("Список пуст. Нечего удалять.");

                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.WriteLine("Текущий список:");
            PrintList();

            Console.Write("Введите ID элемента, после которого нужно удалить все остальные: ");
            int id = int.Parse(Console.ReadLine()!);

            Transport? itemToFind = null;
            foreach (Transport transport in transportList)
            {
                if (transport.ID.Id == id)
                {
                    itemToFind = transport;
                    break;
                }
            }

            if (itemToFind == null)
            {
                Console.WriteLine($"Элемент с индификатором {id} не найден.");
                return;
            }

            int removedCount = transportList.RemoveFromToEnd(itemToFind);
            Console.WriteLine($"Удалено {removedCount} элементов после элемента с ID {id}.");
            PrintList();

            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Метод добавления случайных транспортов на нечётные индексы
        /// </summary>
        public void AddAtOddPositions()
        {
            try
            {
                Console.Write("Введите количество элементов для добавления: ");
                int count = int.Parse(Console.ReadLine()!);

                transportList.AddAtOddPositions(() =>
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

                    return transport;
                }, 
                count);

                Console.WriteLine($"Список до добавления: ");
                PrintList();

                Console.WriteLine($"Добавлен элементы на нечётные позиции.");
                PrintList();

                Console.ReadKey();
                Console.Clear();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Метод для клонирования списка
        /// </summary>
        public void CloneList()
        {
            if (transportList.IsEmpty)
            {
                Console.WriteLine("Список пуст. Клонирование невозможно.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Transport[] transportArray = new Transport[transportList.Count];

            transportList.CopyTo(transportArray, 0);

            DuplexLinkedList<Transport> clonedList = new DuplexLinkedList<Transport>();
            foreach (var transport in transportArray)
            {
                clonedList.Add((Transport)transport.Clone());
            }

            Console.WriteLine("\nОригинальный список:");
            transportList.PrintLinkedList();

            Console.WriteLine("Список успешно клонирован.");

            Console.WriteLine("\nКлонированный список:");
            clonedList.PrintLinkedList();

            Console.WriteLine("Список успешно клонирован.");

            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Метод, очищающий список
        /// </summary>
        public void ClearList()
        {
            transportList.Clear();
            Console.WriteLine("Список очищен.");

            Console.ReadKey();
            Console.Clear();
        }
    }
}
