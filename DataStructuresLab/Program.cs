using CarLibrary;
using DataStructuresLab.Model;
using DataStructuresLab.ModelHashSet;
using System;

namespace DataStructuresLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите тип коллекции, с которым хотите работать:");
                Console.WriteLine("1. Двусвязный список");
                Console.WriteLine("2. Хеш-таблица");
                Console.WriteLine("0. Выход");
                Console.Write("Вы выбрали: ");

                int collectionType;
                if (!int.TryParse(Console.ReadLine(), out collectionType))
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                switch (collectionType)
                {
                    case 1:
                        WorkWithLinkedList();
                        break;
                    case 2:
                        WorkWithHashTable();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        static void WorkWithLinkedList()
        {
            DuplexLinkedList<Transport> transportList = new DuplexLinkedList<Transport>();
            TransportsListActions actions = new TransportsListActions(transportList);

            while (true)
            {
                Console.WriteLine("\nМеню работы с двусвязным списком:");
                Console.WriteLine("1. Создать и заполнить список транспорта");
                Console.WriteLine("2. Распечатать список");
                Console.WriteLine("3. Удалить элементы после указанного");
                Console.WriteLine("4. Добавить элементы на нечётные позиции");
                Console.WriteLine("5. Клонировать список");
                Console.WriteLine("6. Удалить список");
                Console.WriteLine("7. Вернуться к выбору коллекции");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            actions.CreateAndFillList();
                            break;
                        case 2:
                            actions.PrintList();
                            break;
                        case 3:
                            actions.DeleteFromToEnd();
                            break;
                        case 4:
                            actions.AddAtOddPositions();
                            break;
                        case 5:
                            actions.CloneList();
                            break;
                        case 6:
                            actions.ClearList();
                            break;
                        case 7:
                            return;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        static void WorkWithHashTable()
        {
            HashTable<int, Transport> transportsHashTable = new HashTable<int, Transport>();
            TransportsHashTableActions actions = new TransportsHashTableActions(transportsHashTable);

            while (true)
            {
                Console.WriteLine("\nМеню работы с хеш-таблицей:");
                Console.WriteLine("1. Создать и заполнить хеш-таблицу");
                Console.WriteLine("2. Вывести хеш-таблицу");
                Console.WriteLine("3. Удалить транспорт по ID");
                Console.WriteLine("4. Найти транспорт по ID");
                Console.WriteLine("5. Добавить случайные транспорты");
                Console.WriteLine("6. Клонировать хеш-таблицу");
                Console.WriteLine("7. Очистить хеш-таблицу");
                Console.WriteLine("8. Вернуться к выбору коллекции");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    continue;
                }

                try
                {
                    switch (choice)
                    {
                        case 1:
                            actions.CreateAndFillHashTable();
                            break;
                        case 2:
                            actions.PrintHashTable();
                            break;
                        case 3:
                            actions.RemoveTransport();
                            break;
                        case 4:
                            actions.FindTransport();
                            break;
                        case 5:
                            actions.AddRandomTransports();
                            break;
                        case 6:
                            actions.CopyHashTable();
                            break;
                        case 7:
                            actions.ClearHashTable();
                            break;
                        case 8:
                            return;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }
    }
}