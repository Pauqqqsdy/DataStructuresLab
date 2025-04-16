
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    /// <summary>
    /// Класс для внедорожника, является производным класса Transport
    /// </summary>
    public class OffroadCar : Transport
    {
        /// <summary>
        /// Поле полного привода
        /// </summary>
        private bool allWheelDrive;

        /// <summary>
        /// Поле типа внедорожника
        /// </summary>
        private string? offroadType;

        /// <summary>
        /// Список типов внедорожников
        /// </summary>
        static string[] OffroadTypes = { "Багги", "Пикап", "Кроссовер" };

        /// <summary>
        /// Тип внедорожника
        /// </summary>
        public string? OffroadType
        {
            get => offroadType;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Any(char.IsLetter))
                    throw new ArgumentException("Неверно заполненное поле вида внедорожника.");
                offroadType = value;
            }
        }

        /// <summary>
        /// Полный привод
        /// </summary>
        public bool AllWheelDrive
        {
            get => allWheelDrive;
            set
            {
                if (value != true && value != false)
                    throw new ArgumentException("Неверное значение для полного привода.");
                allWheelDrive = value;
            }
        }

        /// <summary>
        /// Конструктор без параметров, который наследует свойства конструктора без параметров класса Transport,
        /// не имеет полного привода и является типом "Багги"
        /// </summary>
        public OffroadCar() : base()
        {
            allWheelDrive = false;
            offroadType = "Багги";
        }

        /// <summary>
        /// Конструктор с параметрами родительского класса Transport и двумя параметрами класса OffroadCar - allWheelDrive и offroadType
        /// </summary>
        /// <param name="id">id транспорта</param>
        /// <param name="brand">бренд</param>
        /// <param name="year">год выпуска</param>
        /// <param name="colour">цвет</param>
        /// <param name="cost">стоимость</param>
        /// <param name="clearance">дорожный просвет</param>
        /// <param name="allWheelDrive">полный привод</param>
        /// <param name="offroadType">тип внедорожника</param>
        public OffroadCar(int id, string brand, int year, string colour, double cost, double clearance, bool allWheelDrive, string offroadType) : base(id, brand, year, colour, cost, clearance)
        {
            AllWheelDrive = allWheelDrive;
            OffroadType = offroadType;
        }

        /// <summary>
        /// Метод для вывода информации об объекте класса OffroadCar
        /// </summary>
        public override void Show()
        {
            string show = $"{ID}, Бренд: {Brand}, Цвет: {Colour}, Год выпуска: {Year}, Стоимость: {Cost} р, " +
                $"Дорожный просвет: {Clearance} мм, Полный привод: {(allWheelDrive ? "да" : "нет")}, Тип внедорожника: {offroadType}.";
            Console.WriteLine(show);
        }

        /// <summary>
        /// Метод для создания объекта класса OffroadCar, вводя информацию с клавиатуры, наследуя поля класса Transport и введя значения для них
        /// </summary>
        public override void ConsoleCreate()
        {
            base.ConsoleCreate();
            Console.Write("Является ли автомобиль полноприводным? 1 - Да, 0 - Нет: ");
            string? input = Console.ReadLine();
            if (input == "1")
            {
                AllWheelDrive = true;
            }
            else if (input == "0")
            {
                AllWheelDrive = false;
            }
            Console.Write("Введите тип внедорожника: ");
            OffroadType = Console.ReadLine();
        }

        /// <summary>
        /// Метод для создания объекта класса OffroadCar с помощью ДСЧ
        /// </summary>
        public override void RandomCreate()
        {
            base.RandomCreate();
            allWheelDrive = rnd.Next(0, 2) == 1;
            offroadType = OffroadTypes[rnd.Next(OffroadTypes.Length)];
        }

        /// <summary>
        /// Метод, возвращающий информацию об объекте класса OffroadCar в виде строки, наследуя поля класса Transport
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $", Полный привод: {(allWheelDrive ? "да" : "нет")}, Тип внедорожника: {offroadType}.";
        }

        /// <summary>
        /// Метод для сравнения объектов класса OffroadCar
        /// </summary>
        /// <param name="obj">сравниваемый объект</param>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not OffroadCar) return false;
            return base.Equals(obj) && this.OffroadType == ((OffroadCar)obj).OffroadType && this.AllWheelDrive == ((OffroadCar)obj).AllWheelDrive;
        }

        /// <summary>
        /// Метод, возвращающий хэш-код для объекта класса OffroadCar
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode, OffroadType, AllWheelDrive);
        }
    }
}
