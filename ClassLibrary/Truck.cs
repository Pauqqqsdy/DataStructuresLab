using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    /// <summary>
    /// Класс для грузового автомобиля, является производным класса Transport
    /// </summary>
    public class Truck : Transport
    {
        /// <summary>
        /// Поле грузоподъёмности грузового автомобиля
        /// </summary>
        private double loadCapacity;

        /// <summary>
        /// Грузоподъёмность
        /// </summary>
        public double LoadCapacity
        {
            get { return loadCapacity; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Грузоподъёмность не может быть отрицательной.");
                loadCapacity = value;
            }
        }

        /// <summary>
        /// Конструктор без параметров для грузового автомобиля, который наследует свойства конструктора без параметров класса Transport
        /// и имеет по умолчанию грузоподъёмность 20000 кг
        /// </summary>
        public Truck() : base()
        {
            LoadCapacity = 20000;
        }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="id">id транспорта</param>
        /// <param name="brand">бренд</param>
        /// <param name="year">год выпуска</param>
        /// <param name="colour">цвет</param>
        /// <param name="cost">стоимость</param>
        /// <param name="clearance">дорожный просвет</param>
        /// <param name="loadCapacity">грузоподъёмность</param>
        public Truck(int id, string brand, int year, string colour, double cost, double clearance, double loadCapacity) : base(id, brand, year, colour, cost, clearance)
        {
            LoadCapacity = loadCapacity;
        }

        /// <summary>
        /// Метод для вывода информации об объекте класса Truck
        /// </summary>
        public override void Show()
        {
            string show = $"{ID}, Бренд: {Brand}, Цвет: {Colour}, Год выпуска: {Year}, Стоимость: {Cost} р, " +
                $"Дорожный просвет: {Clearance} мм, Грузоподъёмность: {LoadCapacity} кг.";
            Console.WriteLine(show); ;
        }

        /// <summary>
        /// Метод для создания объекта класса Truck, вводя информацию с клавиатуры, наследуя поля класса Transport и введя значения для них
        /// </summary>
        public override void ConsoleCreate()
        {
            base.ConsoleCreate();
            Console.Write("Введите грузоподъёмность автомобиля: ");
            LoadCapacity = double.Parse(Console.ReadLine()!);
        }

        /// <summary>
        /// Метод для создания объекта класса Truck с помощью ДСЧ, наследуя поля класса Transport и генерируя значения для них 
        /// </summary>
        public override void RandomCreate()
        {
            base.RandomCreate();
            LoadCapacity = rnd.Next(100, 25000);
        }

        /// <summary>
        /// Метод, возвращающий информацию об объекте класса Truck в виде строки, наследуя поля класса Transport
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $", Грузоподъёмность: {LoadCapacity} кг.";
        }

        /// <summary>
        /// Метод для сравнения объектов класса Truck
        /// </summary>
        /// <param name="obj">сравниваемый объект</param>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Truck) return false;
            return base.Equals(obj) && this.LoadCapacity == ((Truck)obj).LoadCapacity;
        }

        /// <summary>
        /// Метод, возвращающий хэш-код для объекта класса Truck
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), LoadCapacity);
        }
    }
}