using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    /// <summary>
    /// Класс ID 
    /// </summary>
    public class IdNumber
    {
        /// <summary>
        /// Поле id
        /// </summary>
        int id;

        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get => id;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("ID не может быть отрицательным или равным 0.");
                id = value;
            }
        }

        public IdNumber(int number = 0)
        {
            Id = number;
        }

        /// <summary>
        /// Метод для сравнения объектов класса Truck
        /// </summary>
        /// <param name="obj">сравниваемый объект</param>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not IdNumber) return false;
            IdNumber other = (IdNumber)obj;
            return Id == other.Id;
        }

        /// <summary>
        /// Метод, возвращающий хэш-код для объекта класса IdNumber
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        /// <summary>
        /// Метод, возвращающий информацию об объекте класса IdNumber в виде строки
        /// </summary>
        public override string ToString()
        {
            return $"ID: {Id}";
        }
    }
}