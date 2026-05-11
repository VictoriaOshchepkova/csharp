namespace ConsoleApp2_1
{
    /// <summary>
    /// Представляет математическую дробь с операциями сложения, вычитания, умножения и деления.
    /// Дробь автоматически сокращается при создании.
    /// </summary>
    public class Fraction
    {
        private int _numerator; // Приватные атрибуты, _camelCase 
        private int _denominator;

        /// <summary>
        /// Получает или задает числитель дроби.
        /// </summary>
        public int Numerator
        {
            get { return _numerator; }
            set { _numerator = value; }
        }

        /// <summary>
        /// Получает или задает знаменатель дроби.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, когда знаменатель равен 0.</exception>
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
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса Fraction.
        /// Выполняет приведение знаменателя к положительному значению и сокращение дроби.
        /// </summary>
        /// <param name="numerator">Числитель дроби.</param>
        /// <param name="denominator">Знаменатель дроби (не может быть 0).</param>
        /// <exception cref="ArgumentException">Выбрасывается, когда знаменатель равен 0.</exception>
        public Fraction(int numerator, int denominator)
        {
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            int gcd = Gcd(Math.Abs(numerator), Math.Abs(denominator));
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        /// <summary>
        /// Вычисляет наибольший общий делитель двух чисел.
        /// </summary>
        /// <param name="a">Первое число.</param>
        /// <param name="b">Второе число.</param>
        /// <returns>Наибольший общий делитель.</returns>
        private static int Gcd(int a, int b) // PascalCase
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
        /// Вычисляет сумму текущей дроби и другой дроби.
        /// </summary>
        /// <param name="other">Другая дробь.</param>
        /// <returns>Новая дробь, представляющая сумму.</returns>
        public Fraction Sum(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator + other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Вычисляет сумму текущей дроби и целого числа.
        /// </summary>
        /// <param name="number">Целое число.</param>
        /// <returns>Новая дробь, представляющая сумму.</returns>
        public Fraction Sum(int number)
        {
            return Sum(new Fraction(number, 1));
        }

        /// <summary>
        /// Вычисляет разность текущей дроби и другой дроби.
        /// </summary>
        /// <param name="other">Другая дробь.</param>
        /// <returns>Новая дробь, представляющая разность.</returns>
        public Fraction Minus(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator - other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Вычисляет разность текущей дроби и целого числа.
        /// </summary>
        /// <param name="number">Целое число.</param>
        /// <returns>Новая дробь, представляющая разность.</returns>
        public Fraction Minus(int number)
        {
            return Minus(new Fraction(number, 1));
        }

        /// <summary>
        /// Вычисляет произведение текущей дроби и другой дроби.
        /// </summary>
        /// <param name="other">Другая дробь.</param>
        /// <returns>Новая дробь, представляющая произведение.</returns>
        public Fraction Multiply(Fraction other)
        {
            int newNumerator = Numerator * other.Numerator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Вычисляет произведение текущей дроби и целого числа.
        /// </summary>
        /// <param name="number">Целое число.</param>
        /// <returns>Новая дробь, представляющая произведение.</returns>
        public Fraction Multiply(int number)
        {
            return Multiply(new Fraction(number, 1));
        }

        /// <summary>
        /// Вычисляет частное текущей дроби и другой дроби.
        /// </summary>
        /// <param name="other">Другая дробь (делитель).</param>
        /// <returns>Новая дробь, представляющая частное.</returns>
        /// <exception cref="DivideByZeroException">Выбрасывается при делении на ноль.</exception>
        public Fraction Div(Fraction other)
        {
            if (other.Numerator == 0)
                throw new DivideByZeroException("Error: division by zero");

            int newNumerator = Numerator * other.Denominator;
            int newDenominator = Denominator * other.Numerator;
            return new Fraction(newNumerator, newDenominator);
        }

        /// <summary>
        /// Вычисляет частное текущей дроби и целого числа.
        /// </summary>
        /// <param name="number">Целое число (делитель).</param>
        /// <returns>Новая дробь, представляющая частное.</returns>
        /// <exception cref="DivideByZeroException">Выбрасывается при делении на ноль.</exception>
        public Fraction Div(int number)
        {
            return Div(new Fraction(number, 1));
        }

        /// <summary>
        /// Возвращает строковое представление дроби в формате "числитель/знаменатель".
        /// </summary>
        /// <returns>Строковое представление дроби.</returns>
        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }
}