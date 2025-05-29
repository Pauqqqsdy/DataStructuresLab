using CarLibrary;
using DataStructuresLab.BinaryTree;
using DataStructuresLab.Model;
using DataStructuresLab.ModelHashSet;
using System;

namespace DataStructuresLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, int> comparer = Comparer<int>.Default.Compare;

            Tree<int> tree = new Tree<int>();

            Console.WriteLine("Добавление элементов:");
            tree.Insert(10, comparer);
            tree.Insert(20, comparer);
            tree.Insert(5, comparer);
            tree.Add(15);
            Console.WriteLine($"Количество элементов: {tree.Count}");

            Console.WriteLine("\nПеречисление элементов:");
            foreach (var item in tree)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\nПроверка на содержание объекта в дереве:");
            Console.WriteLine("Содержит ли число 10? " + tree.Contains(10));
            Console.WriteLine("Содержит ли число 100? " + tree.Contains(100));

            Console.WriteLine("\nКопирование в массив:");
            int[] array = new int[tree.Count];
            tree.CopyTo(array, 0);
            Console.WriteLine("Элементы скопированы в массив:");
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine("\nУдаление:");
            Console.WriteLine("Удалить 5: " + tree.Remove(5, comparer));
            Console.WriteLine("Удалить 100: " + tree.Remove(100, comparer));
            Console.WriteLine($"Количество после удаления: {tree.Count}");

            Console.WriteLine("\nОчистка:");
            tree.Clear();
            Console.WriteLine($"После очистки количество: {tree.Count}");
            Console.WriteLine("Содержит ли число 10? " + tree.Contains(10));

            while (true)
            {
                Console.WriteLine("Выберите тип коллекции, с которым хотите работать:");
                Console.WriteLine("1. Двусвязный список");
                Console.WriteLine("2. Хеш-таблица");
                Console.WriteLine("3. Дерево");
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
                    case 3:
                        WorkWithBinaryTree();
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

        static void WorkWithBinaryTree()
        {
            BinaryTreeActions actions = new BinaryTreeActions();

            while (true)
            {
                Console.WriteLine("\nМеню работы с бинарным деревом:");
                Console.WriteLine("1. Создать идеально сбалансированное дерево");
                Console.WriteLine("2. Распечатать исходное дерево");
                Console.WriteLine("3. Найти максимальный элемент по году выпуска");
                Console.WriteLine("4. Преобразовать в дерево поиска (BST)");
                Console.WriteLine("5. Удалить элемент по ID");
                Console.WriteLine("6. Очистить дерево");
                Console.WriteLine("7. Добавить новый элемент (Insert)");
                Console.WriteLine("8. Вернуться к выбору коллекции");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                int choice = int.Parse(Console.ReadLine()!);

                try
                {
                    switch (choice)
                    {
                        case 1: 
                            actions.CreateBalancedBinaryTree(); 
                            break;
                        case 2: 
                            actions.PrintOriginalTree(); 
                            break;
                        case 3: 
                            actions.FindMaxYear(); 
                            break;
                        case 4: 
                            actions.ConvertToBST(); 
                            break;
                        case 5: 
                            actions.RemoveNodeById(); 
                            break;
                        case 6: 
                            actions.ClearTree(); 
                            break;
                        case 7: 
                            actions.InsertNewTransport(); 
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