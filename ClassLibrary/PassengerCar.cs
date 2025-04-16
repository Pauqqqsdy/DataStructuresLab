
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    /// <summary>
    /// Класс для легкового автомобиля, является производным класса Transport
    /// </summary>
    public class PassengerCar : Transport
    {
        /// <summary>
        /// Поле количества мест
        /// </summary>
        private int seatsNumber;

        /// <summary>
        /// Поле максимальной скорости
        /// </summary>
        private double maxSpeed;

        /// <summary>
        /// Количество мест
        /// </summary>
        public int SeatsNumber
        {
            get { return seatsNumber; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Количество мест не может быть отрицательным.");
                seatsNumber = value;
            }
        }

        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public double MaxSpeed
        {
            get { return maxSpeed; }
            set
            {
                if (value <= 0 || value >= 1500)
                    throw new ArgumentOutOfRangeException("Несуществующая скорость автомобиля.");
                maxSpeed = value;
            }
        }

        /// <summary>
        /// Конструктор без параметров, который наследует свойства конструктора без параметров класса Transport
        /// и имеет по умолчанию количество мест, равное 2, и максимальную скорость 60 км/ч.
        /// </summary>
        public PassengerCar() : base()
        {
            seatsNumber = 2;
            maxSpeed = 60;
        }

        /// <summary>
        /// Конструктор с параметрами родительского класса Transport и двумя параметрами класса PassengerCar - seatsNumber и maxSpeed
        /// </summary>
        /// <param name="id">id транспорта</param>
        /// <param name="brand">бренд</param>
        /// <param name="year">год выпуска</param>
        /// <param name="colour">цвет</param>
        /// <param name="cost">стоимость</param>
        /// <param name="clearance">дорожный просвет</param>
        /// <param name="seatsNumber">количество мест</param>
        /// <param name="maxSpeed">максимальная скорость</param>
        public PassengerCar(int id, string brand, int year, string colour, double cost, double clearance, int seatsNumber, double maxSpeed) : base(id, brand, year, colour, cost, clearance)
        {
            SeatsNumber = seatsNumber;
            MaxSpeed = maxSpeed;
        }

        /// <summary>
        /// Метод для вывода информации об объекте класса PassengerCar
        /// </summary>
        public override void Show()
        {
            string show = $"{ID}, Бренд: {Brand}, Цвет: {Colour}, Год выпуска: {Year}, Стоимость: {Cost} р, " +
                $"Дорожный просвет: {Clearance} мм, Количество мест: {SeatsNumber}, Максимальная скорость: {MaxSpeed} км/ч.";
            Console.WriteLine(show);
        }

        /// <summary>
        /// Метод для создания объекта класса PassengerCar, вводя информацию с клавиатуры, наследуя поля класса Transport и введя значения для них
        /// </summary>
        public override void ConsoleCreate()
        {
            base.ConsoleCreate();

            Console.Write("Введите количество мест в автомобиле: ");
            SeatsNumber = int.Parse(Console.ReadLine()!);

            Console.Write("Введите максимальную скорость автомобиля: ");
            MaxSpeed = double.Parse(Console.ReadLine()!);
        }

        /// <summary>
        /// Метод для создания объекта класса Truck с помощью ДСЧ, наследуя поля класса Transport и генерируя значения для них
        /// </summary>
        public override void RandomCreate()
        {
            base.RandomCreate();
            SeatsNumber = rnd.Next(1, 10);
            MaxSpeed = rnd.Next(20, 1500);
        }

        /// <summary>
        /// Метод, возвращающий информацию об объекте класса PassengerCar в виде строки, наследуя поля класса Transport
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $", Количество мест: {SeatsNumber}, Максимальная скорость: {MaxSpeed} км/ч.";
        }

        /// <summary>
        /// Метод для сравнения объектов класса PassengerCar
        /// </summary>
        /// <param name="obj">сравниваемый объект</param>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not PassengerCar) return false;
            return base.Equals(obj) && SeatsNumber == ((PassengerCar)obj).SeatsNumber && MaxSpeed == ((PassengerCar)obj).MaxSpeed;
        }

        /// <summary>
        /// Метод, возвращающий хэш-код для объекта класса PassengerCar
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode, SeatsNumber, MaxSpeed);
        }
    }
}
