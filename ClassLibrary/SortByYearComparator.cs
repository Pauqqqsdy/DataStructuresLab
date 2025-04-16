using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    public class SortByYearComparator : IComparer<Transport>
    {
        /// <summary>
        /// Метод, сравнивающий два объекта класса Transport
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public int Compare(Transport? x, Transport? y) => x.Year.CompareTo(y.Year);
    }
}
