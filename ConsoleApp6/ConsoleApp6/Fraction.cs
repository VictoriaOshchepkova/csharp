using System;

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

    /// <summary>
    /// Представляет дробь с поддержкой арифметических операций, нормализации и клонирования.
    /// </summary>
    public class Fraction : ICloneable, IFraction
    {
        /// <summary>
        /// Числитель.
        /// </summary>
        int _numerator;

        /// <summary>
        /// Знаменатель.
        /// </summary>
        int _denominator;

        /// <summary>
        /// Кэшированное вещественное значение дроби.
        /// </summary>
        private double? _cachedRealValue = null;

        /// <summary>
        /// Позволяет получить или установить значение числителя.
        /// </summary>
        /// <remarks>
        /// При установке значения сбрасывается кэшированное вещественное значение.
        /// </remarks>
        public int Numerator
        {
            get { return _numerator; }
            set
            { 
                _numerator = value;
                _cachedRealValue = null;
            }
        }

        /// <summary>
        /// Позволяет получить или установить значение знаменателя.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если знаменатель равен 0.</exception>
        /// <remarks>
        /// При установке значения сбрасывается кэшированное вещественное значение.
        /// </remarks>
        public int Denominator
        {
            get { return _denominator; }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Error: The denominator can't be equal to 0");
                }
                _denominator = value;
                _cachedRealValue = null;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Fraction"/> с указанными числителем и знаменателем.
        /// </summary>
        /// <param name="numerator">Числитель.</param>
        /// <param name="denominator">Знаменатель.</param>
        /// <remarks>
        /// Дробь автоматически нормализуется после установки значений.
        /// </remarks>
        public Fraction(int numerator, int denominator)
        {
            Denominator = denominator;
            Numerator = numerator;
            Normalize();
        }

        /// <summary>
        /// Нормализует дробь: приводит знаменатель к положительному значению и сокращает дробь.
        /// </summary>
        private void Normalize()
        {
            if (Denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }

            int gcd = GCD(Math.Abs(Numerator), Math.Abs(Denominator));
            if (gcd > 1)
            {
                Numerator /= gcd;
                Denominator /= gcd;
            }
        }

        /// <summary>
        /// Вычисляет наибольший общий делитель (НОД) двух целых чисел.
        /// </summary>
        /// <param name="a">Первое целое число.</param>
        /// <param name="b">Второе целое число.</param>
        /// <returns>Наибольший общий делитель чисел <paramref name="a"/> и <paramref name="b"/>.</returns>
        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// Возвращает строковое представление дроби в формате "числитель/знаменатель".
        /// </summary>
        /// <returns>Строковое представление дроби.</returns>
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        /// <summary>
        /// Складывает две дроби.
        /// </summary>
        /// <param name="a">Первая дробь.</param>
        /// <param name="b">Вторая дробь.</param>
        /// <returns>Новая дробь, представляющая сумму <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator +(Fraction a, Fraction b)
        {
            int newNumerator = a.Numerator * b.Denominator + b.Numerator * a.Denominator;
            int newDenominator = a.Denominator * b.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Складывает дробь и целое число.
        /// </summary>
        /// <param name="a">Дробь.</param>
        /// <param name="b">Целое число.</param>
        /// <returns>Новая дробь, представляющая сумму <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator +(Fraction a, int b)
        {
            Fraction fractionB = new Fraction(b, 1);
            return a + fractionB;
        }

        /// <summary>
        /// Складывает целое число и дробь.
        /// </summary>
        /// <param name="b">Целое число.</param>
        /// <param name="a">Дробь.</param>
        /// <returns>Новая дробь, представляющая сумму <paramref name="b"/> и <paramref name="a"/>.</returns>
        public static Fraction operator +(int b, Fraction a)
        {
            return a + b;
        }

        /// <summary>
        /// Вычитает одну дробь из другой.
        /// </summary>
        /// <param name="a">Дробь, из которой вычитают.</param>
        /// <param name="b">Дробь, которую вычитают.</param>
        /// <returns>Новая дробь, представляющая разность <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator -(Fraction a, Fraction b)
        {
            int newNumerator = a.Numerator * b.Denominator - b.Numerator * a.Denominator;
            int newDenominator = a.Denominator * b.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Вычитает целое число из дроби.
        /// </summary>
        /// <param name="a">Дробь.</param>
        /// <param name="b">Целое число.</param>
        /// <returns>Новая дробь, представляющая разность <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator -(Fraction a, int b)
        {
            Fraction fractionB = new Fraction(b, 1);
            return a - fractionB;
        }

        /// <summary>
        /// Вычитает дробь из целого числа.
        /// </summary>
        /// <param name="b">Целое число.</param>
        /// <param name="a">Дробь.</param>
        /// <returns>Новая дробь, представляющая разность <paramref name="b"/> и <paramref name="a"/>.</returns>
        public static Fraction operator -(int b, Fraction a)
        {
            Fraction fractionB = new Fraction(b, 1);
            return fractionB - a;
        }

        /// <summary>
        /// Умножает две дроби.
        /// </summary>
        /// <param name="a">Первая дробь.</param>
        /// <param name="b">Вторая дробь.</param>
        /// <returns>Новая дробь, представляющая произведение <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator *(Fraction a, Fraction b)
        {
            int newNumerator = a.Numerator * b.Numerator;
            int newDenominator = a.Denominator * b.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Умножает дробь на целое число.
        /// </summary>
        /// <param name="a">Дробь.</param>
        /// <param name="b">Целое число.</param>
        /// <returns>Новая дробь, представляющая произведение <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator *(Fraction a, int b)
        {
            Fraction fractionB = new Fraction(b, 1);
            return a * fractionB;
        }

        /// <summary>
        /// Умножает целое число на дробь.
        /// </summary>
        /// <param name="b">Целое число.</param>
        /// <param name="a">Дробь.</param>
        /// <returns>Новая дробь, представляющая произведение <paramref name="b"/> и <paramref name="a"/>.</returns>
        public static Fraction operator *(int b, Fraction a)
        {
            return a * b;
        }

        /// <summary>
        /// Делит одну дробь на другую.
        /// </summary>
        /// <param name="a">Дробь, которую делят.</param>
        /// <param name="b">Дробь, на которую  делят.</param>
        /// <returns>Новая дробь, представляющая частное <paramref name="a"/> и <paramref name="b"/>.</returns>
        /// <exception cref="DivideByZeroException">Выбрасывается, если числитель дроби, на которую делят, равен 0.</exception>
        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Numerator == 0)
            {
                throw new DivideByZeroException("Error: division by zero");
            }

            int newNumerator = a.Numerator * b.Denominator;
            int newDenominator = a.Denominator * b.Numerator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Делит дробь на целое число.
        /// </summary>
        /// <param name="a">Дробь, которую делят.</param>
        /// <param name="b">Целое число, на которое  делят.</param>
        /// <returns>Новая дробь, представляющая частное <paramref name="a"/> и <paramref name="b"/>.</returns>
        /// <exception cref="DivideByZeroException">Выбрасывается, если <paramref name="b"/> равно 0.</exception>
        public static Fraction operator /(Fraction a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Error: division by zero");
            }

            Fraction fractionB = new Fraction(b, 1);
            return a / fractionB;
        }

        /// <summary>
        /// Делит целое число на дробь.
        /// </summary>
        /// <param name="b">Целое число, которое делят.</param>
        /// <param name="a">Дробь, на которую  делят.</param>
        /// <returns>Новая дробь, представляющая частное <paramref name="b"/> и <paramref name="a"/>.</returns>
        public static Fraction operator /(int b, Fraction a)
        {
            Fraction fractionB = new Fraction(b, 1);
            return fractionB / a;
        }

        /// <summary>
        /// Определяет, равны ли два объекта <see cref="Fraction"/>.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns>
        /// true, если <paramref name="obj"/> является дробью с такими же числителем и знаменателем; в противном случае - false.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || (obj is not Fraction))
                return false;

            Fraction other = (Fraction)obj;
            return Numerator == other.Numerator && Denominator == other.Denominator;
        }

        /// <summary>
        /// Возвращает хэш-код для данного объекта <see cref="Fraction"/>.
        /// </summary>
        /// <returns>Хэш-код, вычисленный на основе числителя и знаменателя.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        /// <summary>
        /// Создает новый объект <see cref="Fraction"/>, являющийся копией текущего экземпляра.
        /// </summary>
        /// <returns>Новый объект <see cref="Fraction"/>, копирующий текущий экземпляр.</returns>
        public object Clone()
        {
            return new Fraction(Numerator, Denominator);
        }

        /// <summary>
        /// Возвращает вещественное значение дроби.
        /// </summary>
        /// <returns>Вещественное значение дроби типа <see cref="double"/>.</returns>
        /// <remarks>
        /// Значение кэшируется для последующих вызовов до изменения числителя или знаменателя.
        /// </remarks>
        public double GetRealValue()
        {
            if (_cachedRealValue == null)
            {
                _cachedRealValue = (double)Numerator / Denominator;
            }
            return _cachedRealValue.Value;
        }

        /// <summary>
        /// Устанавливает числитель и знаменатель.
        /// </summary>
        /// <param name="numerator">Числитель.</param>
        /// <param name="denominator">Знаменатель.</param>
        /// <remarks>
        /// Дробь автоматически нормализуется после установки значений.
        /// </remarks>
        public void SetValues(int numerator, int denominator)
        {
            Denominator = denominator;
            Numerator = numerator;
            Normalize();
        }
    }
}