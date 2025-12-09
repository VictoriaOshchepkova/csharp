using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    /// <summary>
    /// Интерфейс для работы с дробями.
    /// </summary>
    public interface IFraction
    {
        /// <summary>
        /// Получает вещественное значение дроби.
        /// </summary>
        /// <returns>Значение дроби в виде числа с плавающей запятой.</returns>
        /// /// <returns>Десятичное значение дроби типа <see cref="double"/>.</returns>
        double GetRealValue();

        /// <summary>
        /// Устанавливает значения числителя и знаменателя.
        /// </summary>
        /// <param name="numerator">Числитель.</param>
        /// <param name="denominator">Знаменатель.</param>
        void SetValues(int numerator, int denominator);
    }
}
