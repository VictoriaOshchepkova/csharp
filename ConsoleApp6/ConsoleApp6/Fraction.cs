using System;

namespace ConsoleApp6
{
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
        /// Складывает текущую дробь с другой.
        /// </summary>
        /// <param name="other">Дробь для сложения.</param>
        /// <returns>Новая дробь, представляющая сумму текущей дроби и <paramref name="other"/>.</returns>
        public Fraction Sum(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator + other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Складывает текущую дробь с целым числом.
        /// </summary>
        /// <param name="number">Целое число.</param>
        /// <returns>Новая дробь, представляющая сумму текущей дроби и <paramref name="number"/>.</returns>
        public Fraction Sum(int number)
        {
            return Sum(new Fraction(number, 1));
        }

        /// <summary>
        /// Складывает две дроби.
        /// </summary>
        /// <param name="a">Первая дробь.</param>
        /// <param name="b">Вторая дробь.</param>
        /// <returns>Новая дробь, представляющая сумму <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator +(Fraction a, Fraction b)
        {
            return a.Sum(b);
        }

        /// <summary>
        /// Складывает дробь и целое число.
        /// </summary>
        /// <param name="a">Дробь.</param>
        /// <param name="b">Целое число.</param>
        /// <returns>Новая дробь, представляющая сумму <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator +(Fraction a, int b)
        {
            return a.Sum(b);
        }

        /// <summary>
        /// Складывает целое число и дробь.
        /// </summary>
        /// <param name="b">Целое число.</param>
        /// <param name="a">Дробь.</param>
        /// <returns>Новая дробь, представляющая сумму <paramref name="b"/> и <paramref name="a"/>.</returns>
        public static Fraction operator +(int b, Fraction a)
        {
            return a.Sum(b);
        }

        /// <summary>
        /// Вычитает другую дробь из текущей.
        /// </summary>
        /// <param name="other">Дробь для вычитания.</param>
        /// <returns>Новая дробь, представляющая разность текущей дроби и <paramref name="other"/>.</returns>
        public Fraction Minus(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator - other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Вычитает целое число из текущей дроби.
        /// </summary>
        /// <param name="number">Целое число.</param>
        /// <returns>Новая дробь, представляющая разность текущей дроби и <paramref name="number"/>.</returns>
        public Fraction Minus(int number)
        {
            return Minus(new Fraction(number, 1));
        }

        /// <summary>
        /// Вычитает одну дробь из другой.
        /// </summary>
        /// <param name="a">Дробь, из которой вычитают.</param>
        /// <param name="b">Дробь, которую вычитают.</param>
        /// <returns>Новая дробь, представляющая разность <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator -(Fraction a, Fraction b)
        {
            return a.Minus(b);
        }

        /// <summary>
        /// Вычитает целое число из дроби.
        /// </summary>
        /// <param name="a">Дробь.</param>
        /// <param name="b">Целое число.</param>
        /// <returns>Новая дробь, представляющая разность <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator -(Fraction a, int b)
        {
            return a.Minus(b);
        }

        /// <summary>
        /// Вычитает дробь из целого числа.
        /// </summary>
        /// <param name="b">Целое число.</param>
        /// <param name="a">Дробь.</param>
        /// <returns>Новая дробь, представляющая разность <paramref name="b"/> и <paramref name="a"/>.</returns>
        public static Fraction operator -(int b, Fraction a)
        {
            return new Fraction(b, 1).Minus(a);
        }

        /// <summary>
        /// Умножает текущую дробь на другую.
        /// </summary>
        /// <param name="other">Дробь для умножения.</param>
        /// <returns>Новая дробь, представляющая произведение текущей дроби на <paramref name="other"/>.</returns>
        public Fraction Multiply(Fraction other)
        {
            int newNumerator = Numerator * other.Numerator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Умножает текущую дробь на целое число.
        /// </summary>
        /// <param name="number">Целое число.</param>
        /// <returns>Новая дробь, представляющая произведение текущей дроби на <paramref name="number"/>.</returns>
        public Fraction Multiply(int number)
        {
            return Multiply(new Fraction(number, 1));
        }

        /// <summary>
        /// Умножает две дроби.
        /// </summary>
        /// <param name="a">Первая дробь.</param>
        /// <param name="b">Вторая дробь.</param>
        /// <returns>Новая дробь, представляющая произведение <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator *(Fraction a, Fraction b)
        {
            return a.Multiply(b);
        }

        /// <summary>
        /// Умножает дробь на целое число.
        /// </summary>
        /// <param name="a">Дробь.</param>
        /// <param name="b">Целое число.</param>
        /// <returns>Новая дробь, представляющая произведение <paramref name="a"/> и <paramref name="b"/>.</returns>
        public static Fraction operator *(Fraction a, int b)
        {
            return a.Multiply(b);
        }

        /// <summary>
        /// Умножает целое число на дробь.
        /// </summary>
        /// <param name="b">Целое число.</param>
        /// <param name="a">Дробь.</param>
        /// <returns>Новая дробь, представляющая произведение <paramref name="b"/> и <paramref name="a"/>.</returns>
        public static Fraction operator *(int b, Fraction a)
        {
            return a.Multiply(b);
        }

        /// <summary>
        /// Делит текущую дробь на другую.
        /// </summary>
        /// <param name="other">Дробь, на которую нужно разделить.</param>
        /// <returns>Новая дробь, представляющая частное текущей дроби и <paramref name="other"/>.</returns>
        /// <exception cref="DivideByZeroException">Выбрасывается, если числитель дроби, на которую делят, равен 0.</exception>
        public Fraction Div(Fraction other)
        {
            if (other.Numerator == 0)
            {
                throw new DivideByZeroException("Error: division by zero");
            }

            int newNumerator = Numerator * other.Denominator;
            int newDenominator = Denominator * other.Numerator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Делит текущую дробь на целое число.
        /// </summary>
        /// <param name="number">Целое число.</param>
        /// <returns>Новая дробь, представляющая частное текущей дроби и <paramref name="number"/>.</returns>
        public Fraction Div(int number)
        {
            return Div(new Fraction(number, 1));
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
            return a.Div(b);
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
            return a.Div(b);
        }

        /// <summary>
        /// Делит целое число на дробь.
        /// </summary>
        /// <param name="b">Целое число, которое делят.</param>
        /// <param name="a">Дробь, на которую  делят.</param>
        /// <returns>Новая дробь, представляющая частное <paramref name="b"/> и <paramref name="a"/>.</returns>
        public static Fraction operator /(int b, Fraction a)
        {
            return new Fraction(b, 1).Div(a);
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