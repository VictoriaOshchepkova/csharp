using System;

namespace ConsoleApp1
{
    public class Methods
    {
        // ЗАДАНИЕ 1

        // №2. Сумма знаков
        public int sumLastNums(int x)
        {
            return (x % 10) + (x % 100 / 10);
        }

        // №4. Есть ли позитив
        public bool isPositive(int x)
        {
            return x > 0;
        }

        // №6. Большая буква
        public bool isUpperCase(char x)
        {
            return 'A' <= x && x <= 'Z';
        }

        // №8. Делитель
        public bool isDivisor(int a, int b)
        {
            return (a % b == 0 && b != 0) || (b % a == 0 && a != 0);
        }

        // №10. Многократный вызов
        public int lastNumSum(int a, int b)
        {
            return (a % 10) + (b % 10);
        }

        // ЗАДАНИЕ 2

        // №2. Безопасное деление
        public double safeDiv(int x, int y)
        {
            if (y == 0)
                return 0;
            return (double)x / y;
        }

        // №4. Строка сравнения
        public String makeDecision(int x, int y)
        {
            if (x < y)
                return $"{x}<{y}";
            if (y < x)
                return $"{y}<{x}";
            return $"{x}=={y}";
        }

        // №6. Тройная сумма
        public bool sum3(int x, int y, int z)
        {
            if (x + y == z)
                return true;
            if (y + z == x)
                return true;
            if (x + z == y)
                return true;
            return false;
        }

        // №8. Возраст
        public String age(int x)
        {
            int lastDigit = x % 10;
            int twoLastDigit = x % 100;

            switch (lastDigit)
            {
                case 1 when twoLastDigit != 11:
                    return $"{x} год";
                case 2 when twoLastDigit != 12:
                case 3 when twoLastDigit != 13:
                case 4 when twoLastDigit != 14:
                    return $"{x} года";
                default:
                    return $"{x} лет";
            }
        }

        // №10. Вывод дней недели
        public void printDays(String x)
        {
            switch (x.ToLower())
            {
                case "понедельник":
                    Console.WriteLine("понедельник");
                    Console.WriteLine("вторник");
                    Console.WriteLine("среда");
                    Console.WriteLine("четверг");
                    Console.WriteLine("пятница");
                    Console.WriteLine("суббота");
                    Console.WriteLine("воскресенье");
                    break;
                case "вторник":
                    Console.WriteLine("вторник");
                    Console.WriteLine("среда");
                    Console.WriteLine("четверг");
                    Console.WriteLine("пятница");
                    Console.WriteLine("суббота");
                    Console.WriteLine("воскресенье");
                    break;
                case "среда":
                    Console.WriteLine("среда");
                    Console.WriteLine("четверг");
                    Console.WriteLine("пятница");
                    Console.WriteLine("суббота");
                    Console.WriteLine("воскресенье");
                    break;
                case "четверг":
                    Console.WriteLine("четверг");
                    Console.WriteLine("пятница");
                    Console.WriteLine("суббота");
                    Console.WriteLine("воскресенье");
                    break;
                case "пятница":
                    Console.WriteLine("пятница");
                    Console.WriteLine("суббота");
                    Console.WriteLine("воскресенье");
                    break;
                case "суббота":
                    Console.WriteLine("суббота");
                    Console.WriteLine("воскресенье");
                    break;
                case "воскресенье":
                    Console.WriteLine("воскресенье");
                    break;
                default:
                    Console.WriteLine("это не день недели");
                    break;
            }
        }

        // ЗАДАНИЕ 3

        // №2. Число наоборот
        public String reverseListNums(int x)
        {
            string result = "";
            if (x >= 0)
            {
                for (int i = x; i > -1; i--)
                {
                    result += i;
                    if (i != 0)
                        result += " ";
                }
                return result;
            }
            for (int i = x; i < 1; i++)
            {
                result += i;
                if (i != 0)
                    result += " ";
            }
            return result;
        }

        // №4. Степень числа
        public int pow(int x, int y)
        {
            if (y < 0)
                return 0;
            if (y == 0)
                return 1;

            int result = 1;
            for (int i = 0; i < y; i++)
            {
                result *= x;
            }
            return result;
        }

        // №6. Одинаковость
        public bool equalNum(int x)
        {
            if (x < 0)
                return false;
            if (x < 10)
                return true;

            int lastDigit = x % 10;
            int rest = x / 10;

            while (rest > 0)
            {
                int currentDigit = rest % 10;
                if (currentDigit != lastDigit)
                    return false;
                rest /= 10;
            }
            return true;
        }

        // №8. Левый треугольник
        public void leftTriange(int x)
        {
            for (int i = 1; i <= x; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        // №10. Угадайка
        public void guessGame()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 10);
            int answer = -1;
            int attempts = 0;

            Console.WriteLine(" Введите число от 0 до 9:");
            while (answer != randomNumber)
            {
                while (true)
                {
                    try
                    {
                        answer = int.Parse(Console.ReadLine());
                        attempts++;
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Error: Invalid input");
                    }
                }
                if (answer == randomNumber)
                {
                    Console.WriteLine(" Вы угадали!");
                    Console.WriteLine($" Вы отгадали число за {attempts} попытки");
                }
                else
                {
                    Console.WriteLine(" Вы не угадали, введите число от 0 до 9:");
                }

            }
        }

        // ЗАДАНИЕ 4

        // №2. Поиск последнего значения
        public int findLast(int[] arr, int x)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (arr[i] == x)
                    return i;
            }
            return -1;
        }

        // №4. Добавление в массив
        public int[] add(int[] arr, int x, int pos)
        {
            int[] result = new int[arr.Length + 1];
            int currentPos = 0;

            for (int i = 0; i <= result.Length - 1; i++)
            {
                if (i == pos)
                    result[i] = x;
                else
                {
                    result[i] = arr[currentPos];
                    currentPos++;
                }
            }
            return result;
        }

        // №6. Реверс
        public void reverse(int[] arr)
        {
            int arrLength = arr.Length - 1;
            for (int i = 0; i < arr.Length / 2; i++)
            {
                (arr[i], arr[arrLength - i]) = (arr[arrLength - i], arr[i]);
            }
        }

        // №8. Объединение
        public int[] concat(int[] arr1, int[] arr2)
        {
            int[] result = new int[arr1.Length + arr2.Length];
            int currentPos = arr1.Length;

            for (int i = 0; i < arr1.Length; i++)
            {
                result[i] = arr1[i];
            }

            for (int i = 0; i < arr2.Length; i++)
            {
                result[currentPos] = arr2[i];
                currentPos++;
            }

            return result;
        }

        // №10. Удалить негатив
        public int[] deleteNegative(int[] arr)
        {
            int countPositive = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] >= 0)
                    countPositive++;
            }

            int[] result = new int[countPositive];
            int currentPos = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] >= 0)
                {
                    result[currentPos] = arr[i];
                    currentPos++;
                }
            }
            return result;
        }

        // Вывод массива
        public void printArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]} ");
            }
            Console.WriteLine();
        }

        // Ввод массива
        public int[] inputArr(string input)
        {
            string[] stringArr = input.Split(' ');
            int[] arr = new int[stringArr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = int.Parse(stringArr[i]);
            }
            return arr;
        }
    }
}