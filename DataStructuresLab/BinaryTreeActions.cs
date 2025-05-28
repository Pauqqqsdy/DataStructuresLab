using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarLibrary;
using DataStructuresLab.BinaryTree;

namespace DataStructuresLab
{
    internal class BinaryTreeActions
    {
        private Tree<Transport> transportTree;

        public BinaryTreeActions()
        {
            transportTree = new Tree<Transport>();
        }

        /// <summary>
        /// Создание идеально сбалансированного дерева
        /// </summary>
        public void CreateBalancedBinaryTree()
        {
            Console.Write("Введите количество транспортных средств: ");
            int count = int.Parse(Console.ReadLine()!);

            transportTree.Clear();

            Func<Transport> getData = () =>
            {
                Random rnd = new Random();
                Transport transport;

                int type = rnd.Next(1, 5);
                switch (type)
                {
                    case 1: 
                        transport = new Transport(); 
                        break;
                    case 2: 
                        transport = new Truck(); 
                        break;
                    case 3: 
                        transport = new OffroadCar(); 
                        break;
                    case 4: 
                        transport = new PassengerCar(); 
                        break;
                    default: 
                        transport = new Transport(); 
                        break;
                }

                transport.RandomCreate();
                return transport;
            };

            transportTree.Root = transportTree.CreateIdealTree(count, getData);
            transportTree.Count = count;

            Console.WriteLine($"Идеальное дерево создано из {count} элементов.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Вывод дерева по уровням
        /// </summary>
        public void PrintOriginalTree()
        {
            if (transportTree.Root == null)
            {
                Console.WriteLine("Дерево пустое.");
            }
            else
            {
                Console.WriteLine("Исходное дерево:");
                Console.WriteLine(transportTree.PrintByLevels());
            }
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Найти максимальный элемент по году выпуска
        /// </summary>
        public void FindMaxYear()
        {
            if (transportTree.Root == null)
            {
                Console.WriteLine("Дерево пустое.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Transport maxTransport = transportTree.FindMax((x, y) => x.Year.CompareTo(y.Year));
            Console.WriteLine("Транспорт с последним годом выпуска:");
            Console.WriteLine(maxTransport.ToString());
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Преобразование в дерево поиска
        /// </summary>
        public void ConvertToBST()
        {
            if (transportTree.Root == null)
            {
                Console.WriteLine("Дерево пустое. Невозможно преобразовать в дерево поиска.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Tree<Transport> bstTree = transportTree.ConvertToBST((x, y) => x.ID.Id.CompareTo(y.ID.Id));

            Console.WriteLine("Преобразованное дерево поиска:");
            Console.WriteLine(bstTree.PrintByLevels());

            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Удаление элемента из дерева по ID
        /// </summary>
        public void RemoveNodeById()
        {
            if (transportTree.Root == null)
            {
                Console.WriteLine("Дерево пустое. Нечего удалять.");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.Write("Введите ID транспорта для удаления: ");
            int id = int.Parse(Console.ReadLine()!);

            var transport = new Transport { ID = new IdNumber(id) };

            bool removed = transportTree.Remove(transport, (x, y) => x.ID.Id.CompareTo(y.ID.Id));

            if (removed)
            {
                Console.WriteLine($"Транспорт с ID {id} успешно удален.");
            }
            else
            {
                Console.WriteLine($"Транспорт с ID {id} не найден.");
            }

            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Очистка дерева
        /// </summary>
        public void ClearTree()
        {
            transportTree.Clear();
            Console.WriteLine("Дерево очищено.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Добавление нового транспорта
        /// </summary>
        public void InsertNewTransport()
        {
            Tree<Transport> bstTree = transportTree.ConvertToBST((x, y) => x.ID.Id.CompareTo(y.ID.Id));

            Console.WriteLine("Создайте новый транспорт:");
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
                    break;
                case 2: 
                    transport = new Truck(); 
                    break;
                case 3: 
                    transport = new OffroadCar(); 
                    break;
                case 4: 
                    transport = new PassengerCar(); 
                    break;
                default: 
                    transport = new Transport(); 
                    break;
            }

            transport.RandomCreate();
            Console.WriteLine($"Добавляемый транспорт: {transport}");

            bstTree.Insert(transport, (x, y) => x.ID.Id.CompareTo(y.ID.Id));

            Console.WriteLine("Обновлённое дерево поиска:");
            Console.WriteLine(bstTree.PrintByLevels());

            Console.ReadKey();
            Console.Clear();
        }
    }
}
