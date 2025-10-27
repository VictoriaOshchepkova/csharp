using System;

namespace ConsoleApp3
{
    public class Matrix
    {
        int[,] _data;

        public int[,] Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public Matrix() { }

        public Matrix(int n, int m)
        {
            _data = new int[n, m];
            Console.WriteLine($"Введите {n * m} элементов массива {n}x{m} (по столбцам):");

            for (int j = 0; j < m; ++j)
            {
                for (int i = 0; i < n; ++i)
                {
                    int elem;
                    while (true)
                    {
                        Console.Write($"Элемент [{i},{j}]: ");

                        try
                        {
                            elem = int.Parse(Console.ReadLine());
                            _data[i, j] = elem;
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: Invalid input");
                        }
                    }
                }
            }
        }

        public Matrix(int n, string task)
        {
            _data = new int[n, n];
            int curr;

            for (int diag = 0; diag < 2 * n - 1; ++diag)
            {
                if (diag % 2 == 0)
                {
                    curr = 1;
                    for (int i = 0; i <= diag; i++)
                    {
                        int j = diag - i;
                        if (i < n && j < n)
                        {
                            _data[i, j] = curr;
                            ++curr;
                        }
                    }
                }
                else
                {
                    curr = 1;
                    for (int i = diag; i >= 0; i--)
                    {
                        int j = diag - i;
                        if (i < n && j < n)
                        {
                            _data[i, j] = curr++;
                        }
                    }
                }
            }
        }

        public Matrix(int n)
        {
            _data = new int[n, n];
            Random rand = new Random();

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    _data[i, j] = ThreeDigitNumberIncreasing(rand);
                }
            }
        }

        public Matrix(int[] arr)
        {
            int n = arr.Length;
            _data = new int[n, n];

            for (int i = 0; i < n - 1; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    _data[i, j] = 1;
                }
            }

            for (int j = 0; j < n; ++j)
            {
                int rest;

                rest = arr[j] - (n - 1);
                if (rest == 0)
                {
                    _data[n - 2, j] = 2;
                    rest = -1;
                }
                _data[n - 1, j] = rest;
            }
        }

        private int ThreeDigitNumberIncreasing(Random rand)
        {
            while (true)
            {
                int num = rand.Next(100, 1000);
                int digit1 = num / 100;
                int digit2 = num / 10 % 10;
                int digit3 = num % 10;

                if (digit1 < digit2 && digit2 < digit3)
                {
                    return num;
                }
            }
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a._data.GetLength(0) != b._data.GetLength(0) || a._data.GetLength(1) != b._data.GetLength(1))
                throw new InvalidOperationException("Error: Different sizes of matrices");

            int rows = a._data.GetLength(0);
            int cols = a._data.GetLength(1);
            Matrix result = new Matrix();
            result._data = new int[rows, cols];

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    result._data[i, j] = a._data[i, j] - b._data[i, j];
                }
            }

            return result;
        }

        public static Matrix operator *(int number, Matrix a)
        {
            int rows = a._data.GetLength(0);
            int cols = a._data.GetLength(1);
            Matrix result = new Matrix();
            result._data = new int[rows, cols];

            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    result._data[i, j] = a._data[i, j] * number;
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix matrix, int number)
        {
            return number * matrix;
        }

        public Matrix Transpose()
        {
            int rows = _data.GetLength(0);
            int cols = _data.GetLength(1);
            Matrix result = new Matrix();
            result._data = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result._data[j, i] = _data[i, j];
                }
            }

            return result;
        }

        public override string ToString()
        {
            string result = "";

            int rows = Data.GetLength(0);
            int cols = Data.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result += $"\t{_data[i, j]}";
                }
                result += "\n";
            }

            return result;
        }
    }
}
