using CarLibrary;
using DataStructuresLab.Model;

namespace DataStructuresLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var transportList = new DuplexLinkedList<Transport>();
            for (int i = 0; i < 3; i ++)
            {
                Transport transport = new Transport();
                transport.RandomCreate();
                transportList.Add(transport);
            }

            transportList.PrintAll();

            Console.WriteLine();

            var reverseList = transportList.Reverse();

            reverseList.PrintAll();

            Console.ReadLine();
        }
    }
}
