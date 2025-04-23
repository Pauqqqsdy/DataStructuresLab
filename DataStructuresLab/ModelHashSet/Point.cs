using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresLab.ModelHashSet
{
    class Point<T>
    {
        public T? Data { get; set; }

        public Point<T>? Next { get; set; }

        public Point()
        {
            Data = default(T);
            Next = null;
        }

        public Point(T? data)
        {
            Data = data;
            Next = null;
        }

        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }

        public override int GetHashCode()
        {
            return Data?.GetHashCode() ?? 0;
        }
    }
}
