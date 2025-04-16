using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLibrary
{
    /// <summary>
    /// Интерфейс
    /// </summary>
    public interface ICreateItems
    {
        /// <summary>
        /// Ручной ввод
        /// </summary>
        void ConsoleCreate();

        /// <summary>
        /// Ввод с помощь ДСЧ
        /// </summary>
        void RandomCreate();
    }
}
