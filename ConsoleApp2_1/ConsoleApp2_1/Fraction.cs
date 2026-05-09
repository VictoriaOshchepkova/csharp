namespace ConsoleApp2_1
{
    public class Fraction
    {
        private int _numerator; // Приватные атрибуты, _camelCase 
        private int _denominator;

        public int Numerator
        {
            get { return _numerator; }
            set { _numerator = value; }
        }

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

        public Fraction Sum(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator + other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Sum(int number)
        {
            return Sum(new Fraction(number, 1));
        }

        public Fraction Minus(Fraction other)
        {
            int newNumerator = Numerator * other.Denominator - other.Numerator * Denominator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Minus(int number)
        {
            return Minus(new Fraction(number, 1));
        }

        public Fraction Multiply(Fraction other)
        {
            int newNumerator = Numerator * other.Numerator;
            int newDenominator = Denominator * other.Denominator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Multiply(int number)
        {
            return Multiply(new Fraction(number, 1));
        }

        public Fraction Div(Fraction other)
        {
            if (other.Numerator == 0)
                throw new DivideByZeroException("Error: division by zero");

            int newNumerator = Numerator * other.Denominator;
            int newDenominator = Denominator * other.Numerator;
            return new Fraction(newNumerator, newDenominator);
        }

        public Fraction Div(int number)
        {
            return Div(new Fraction(number, 1));
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }
    }
}
