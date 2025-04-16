using CarLibrary;
using DataStructuresLab.Model;


namespace DataStructuresLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DuplexLinkedList<Transport> transportList = new DuplexLinkedList<Transport>();
            TransportsListActions actions = new TransportsListActions(transportList);

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Создать и заполнить список транспорта");
                Console.WriteLine("2. Распечатать список");
                Console.WriteLine("3. Удалить элементы после указанного");
                Console.WriteLine("4. Добавить элементы на нечётные позиции");
                Console.WriteLine("5. Клонировать список");
                Console.WriteLine("6. Удалить список");
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

                        case 0:
                            return;

                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}
