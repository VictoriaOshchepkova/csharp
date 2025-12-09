using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    /// <summary>
    /// Интерфейс, определяющий возможность мяуканья.
    /// </summary>
    public interface IMeowable
    {
        /// <summary>
        /// Издает одиночное мяуканье.
        /// </summary>
        void Meow();
    }
}
