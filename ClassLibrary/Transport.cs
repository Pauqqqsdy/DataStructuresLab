
namespace CarLibrary
{
    /// <summary>
    /// Базовый класс
    /// </summary>
    public class Transport : ICreateItems, IComparable<Transport>, ICloneable
    {
        protected static Random rnd = new Random();
        /// <summary>
        /// Поле бренда автомобиля
        /// </summary>
        protected string? brand;
        /// <summary>
        /// Поле цвета автомобиля
        /// </summary>
        protected string? colour;
        /// <summary>
        /// Поле года выпуска автомобиля
        /// </summary>
        protected int year;
        /// <summary>
        /// Поле стоимости автомобиля
        /// </summary>
        protected double cost;
        /// <summary>
        /// Поле дорожного просвета автомобиля
        /// </summary>
        protected double clearance;

        public IdNumber ID { get; set; }

        /// <summary>
        /// Список брендов
        /// </summary>
        static string[] Brands = { "Toyota", "Ford", "Honda", "Chevrolet", "Nissan", "BMW", "Mercedes-Benz", "Volkswagen",
        "Audi", "Hyundai", "Kia", "Subaru", "Mazda", "Peugeot", "Jaguar", "Land Rover", "Porsche", "Volvo", "Fiat", "Tesla" };

        /// <summary>
        /// Список цветов
        /// </summary>
        static string[] Colours = { "Красный", "Синий", "Зелёный", "Чёрный", "Белый", "Серый",
        "Жёлтый", "Оранжевый", "Фиолетовый", "Бежевый", "Коричневый", "Розовый"};

        /// <summary>
        /// Бренд
        /// </summary>
        public string? Brand
        {
            get => brand;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Any(char.IsLetter))
                    throw new ArgumentException("Неверно заполненное поле марки автомобиля.");
                brand = value;
            }
        }

        /// <summary>
        /// Цвет
        /// </summary>
        public string? Colour
        {
            get => colour;
            set
            {
                if (string.IsNullOrEmpty(value) || !value.All(char.IsLetter))
                    throw new ArgumentException("Неверно заполненное поле цвета автомобиля.");
                colour = value;
            }
        }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int Year
        {
            get => year;
            set
            {
                if (value < 1885 || value > 2025)
                    throw new ArgumentOutOfRangeException("Выпуск в данный год невозможен.");
                year = value;
            }
        }

        /// <summary>
        /// Стоимость
        /// </summary>
        public double Cost
        {
            get => cost;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Стоимость не может быть отрицательной.");
                cost = value;
            }
        }

        /// <summary>
        /// Дорожный просвет
        /// </summary>
        public double Clearance
        {
            get => clearance;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Дорожный просвет не может быть отрицательным.");
                clearance = value;
            }
        }

        /// <summary>
        /// Конструктор без параметров, у которого по умолчанию бренд Лада, год выпуска 2010, синий цвет, 
        /// стоимость 150000 р и дорожный просвет 115 мм
        /// </summary>
        public Transport()
        {
            ID = new IdNumber(1);
            Brand = "Лада";
            Year = 2010;
            Colour = "Синий";
            Cost = 150000;
            Clearance = 115;
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
        public Transport(int id, string brand, int year, string colour, double cost, double clearance)
        {
            ID = new IdNumber(id);
            Brand = brand;
            Year = year;
            Colour = colour;
            Cost = cost;
            Clearance = clearance;
        }

        /// <summary>
        /// Метод для вывода информации об объекте класса Transport
        /// </summary>
        public virtual void Show()
        {
            Console.WriteLine($"{ID}, Бренд: {Brand}, Цвет: {Colour}, Год выпуска: {Year}, Стоимость: {Cost} р, Дорожный просвет: {Clearance} мм. ");
        }

        /// <summary>
        /// Метод для создания объекта класса Transport, вводя информацию с клавиатуры
        /// </summary>
        public virtual void ConsoleCreate()
        {
            Console.Write("Введите ID автомобиля: ");
            ID = new IdNumber(int.Parse(Console.ReadLine()!));

            Console.Write("Введите марку автомобиля: ");
            Brand = Console.ReadLine();

            Console.Write("Введите цвет автомобиля: ");
            Colour = Console.ReadLine();

            Console.Write("Введите год выпуска: ");
            Year = int.Parse(Console.ReadLine()!);

            Console.Write("Введите стоимость: ");
            Cost = double.Parse(Console.ReadLine()!);

            Console.Write("Введите дорожный просвет автомобиля: ");
            Clearance = double.Parse(Console.ReadLine()!);
        }

        /// <summary>
        /// Метод для создания объекта класса Transport с помощью ДСЧ
        /// </summary>
        public virtual void RandomCreate()
        {
            ID = new IdNumber(rnd.Next(1, 1000));
            Brand = Brands[rnd.Next(Brands.Length)];
            Colour = Colours[rnd.Next(Colours.Length)];
            Year = rnd.Next(1885, 2025);
            Cost = rnd.Next(250000, 50000000);
            Clearance = rnd.Next(10, 900);
        }

        /// <summary>
        /// Метод, возвращающий информацию об объекте класса Transport в виде строки
        /// </summary>
        public override string ToString()
        {
            return $"{ID}, Бренд: {Brand}, Цвет: {Colour}, Год выпуска: {Year}, Стоимость: {Cost} р, Дорожный просвет: {Clearance} мм.";
        }

        /// <summary>
        /// Метод для сравнения объектов класса Transport
        /// </summary>
        /// <param name="obj">сравниваемый объект</param>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not Transport) return false;
            Transport other = (Transport)obj;
            return Brand == other.Brand && Colour == other.Colour && Year == other.Year
                && Cost == other.Cost && Clearance == other.Clearance && ID.Equals(other.ID);
        }

        /// <summary>
        /// Метод, возвращающий хэш-код для объекта класса Transport
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(ID, Brand, Colour, Year, Cost, Clearance, ID);
        }

        public int CompareTo(Transport? other)
        {
            if (other == null) return -1;
            return Brand.CompareTo(other.Brand);
        }

        public object Clone()
        {
            return new Transport(this.ID.Id, Brand, Year, Colour, Cost, Clearance);
        }

        public object ShallowCopy()
        {
            return this.MemberwiseClone();
        }
    }
}