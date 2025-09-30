using System;

namespace ConsoleApp1
{
    class Program
    {
        // ИНТЕРФЕЙС
        public static void Main(string[] args)
        {
            Methods Methods1 = new Methods();
            int task = 0;
            int subtask = 0;

            Console.WriteLine(@"
             ЛАБОРАТОРНАЯ РАБОТА №1
          Процедурное программирование
 -----------------------------------------------
 Задание №1. Методы;
 Задание №2. Условия;
 Задание №3. Циклы;
 Задание №4. Массивы.
 -----------------------------------------------");
            while (true)
            {
                while (true)
                {
                    Console.WriteLine(" Выберите номер задания (число), для исполнения:");

                    try
                    {
                        task = int.Parse(Console.ReadLine());
                        if (task >= 1 && task <= 4)
                            break;
                        Console.WriteLine(" Error: Invalid input");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Error: Invalid input");
                    }
                }

                while (true)
                {
                    Console.WriteLine(" Выберите номер подзадания (число 2, 4, 6, 8 или 10):");
                    try
                    {
                        subtask = int.Parse(Console.ReadLine());
                        if (subtask >= 2 && subtask <= 10 && subtask % 2 == 0)
                            break;
                        Console.WriteLine(" Error: Invalid input");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(" Error: Invalid input");
                    }
                }

                Console.WriteLine($" Задание №{task}. Подзадание №{subtask}");

                switch (task)
                {
                    case 1:
                        switch (subtask)
                        {
                            case 2:
                                Console.WriteLine(" Сумма двух последних знаков числа x");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целоe число x > 9:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        if (x >= 10)
                                        {
                                            Console.WriteLine($" Результат: {Methods1.sumLastNums(x)}");
                                            break;
                                        }
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 4:
                                Console.WriteLine(" Положительное ли число x?");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целоe число x:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        Console.WriteLine($" Результат: {Methods1.isPositive(x)}");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 6:
                                Console.WriteLine(" Является ли символ x большой буквой?");
                                while (true)
                                {
                                    Console.WriteLine(" Введите символ x:");
                                    try
                                    {
                                        string x = Console.ReadLine();
                                        if (x.Length == 1)
                                        {
                                            char symbol = x[0];
                                            Console.WriteLine($" Результат: {Methods1.isUpperCase(symbol)}");
                                            break;
                                        }
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 8:
                                Console.WriteLine(" Делит ли любое из чисел x и y другое нацело?");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целые числа x и y:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        int y = int.Parse(Console.ReadLine());
                                        Console.WriteLine($" Результат: {Methods1.isDivisor(x, y)}");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 10:
                                Console.WriteLine(" Сумма цифр 5 чисел из разряда единиц");
                                int a = 0;
                                while (true)
                                {
                                    Console.WriteLine(" Введите 1-e целое число:");
                                    try
                                    {
                                        a = int.Parse(Console.ReadLine());
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                for (int i = 2; i < 6; i++)
                                {
                                    while (true)
                                    {
                                        Console.WriteLine($" Введите {i}-e целое число:");
                                        try
                                        {
                                            int x = int.Parse(Console.ReadLine());
                                            a = Methods1.lastNumSum(a, x);
                                            break;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(" Error: Invalid input");
                                        }
                                    }
                                }
                                Console.WriteLine($" Результат: {a}");
                                break;

                        }
                        break;
                    case 2:
                        switch (subtask)
                        {
                            case 2:
                                Console.WriteLine(" Безопасное деление x на у");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целые числа x и y:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        int y = int.Parse(Console.ReadLine());
                                        Console.WriteLine($" Результат: {Methods1.safeDiv(x, y)}");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 4:
                                Console.WriteLine(" Сравнение чисел x и y");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целые числа x и y:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        int y = int.Parse(Console.ReadLine());
                                        Console.WriteLine($" Результат: {Methods1.makeDecision(x, y)}");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 6:
                                Console.WriteLine(" Можни ли сложить два любых числа так, чтобы получить третье?");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целые числа x, y и z:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        int y = int.Parse(Console.ReadLine());
                                        int z = int.Parse(Console.ReadLine());
                                        Console.WriteLine($" Результат: {Methods1.sum3(x, y, z)}");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 8:
                                Console.WriteLine(" Возраст (год/года/лет)");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целое положительное число x:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        if (x >= 0)
                                        {
                                            Console.WriteLine($" Результат: {Methods1.age(x)}");
                                            break;
                                        }
                                        Console.WriteLine(" Error: Invalid input");

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 10:
                                Console.WriteLine(" Вывод дней недели, начиная с дня x");
                                Console.WriteLine(" Введите день недели x:");
                                string day = Console.ReadLine();
                                Console.WriteLine(" Результат:");
                                Methods1.printDays(day);
                                break;
                        }
                        break;
                    case 3:
                        switch (subtask)
                        {
                            case 2:
                                Console.WriteLine(" Все числа от x до 0");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целое число x:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        Console.WriteLine($" Результат: {Methods1.reverseListNums(x)}");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 4:
                                Console.WriteLine(" Число x в степени y");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целые цисла x и y:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        int y = int.Parse(Console.ReadLine());
                                        Console.WriteLine($" Результат: {Methods1.pow(x, y)}");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 6:
                                Console.WriteLine(" Все ли знаки в числе одинаковы?");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целое число x:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        Console.WriteLine($" Результат: {Methods1.equalNum(x)}");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 8:
                                Console.WriteLine(" Треугольник из звездочек размером x на x");
                                while (true)
                                {
                                    Console.WriteLine(" Введите целое положительное число x:");
                                    try
                                    {
                                        int x = int.Parse(Console.ReadLine());
                                        if (x >= 0)
                                        {
                                            Console.WriteLine(" Результат:");
                                            Methods1.leftTriange(x);
                                            break;
                                        }
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 10:
                                Console.WriteLine(" Угадайте число от 0 до 9");
                                Methods1.guessGame();
                                break;
                        }
                        break;
                    case 4:
                        switch (subtask)
                        {
                            case 2:
                                Console.WriteLine(" Поиск индекса последнего вхождения числа x в массив arr");
                                while (true)
                                {
                                    Console.WriteLine(" Введите массив arr (через пробел) из целых чисел и целое число x:");
                                    try
                                    {
                                        int[] arr = Methods1.inputArr(Console.ReadLine());
                                        int x = int.Parse(Console.ReadLine());
                                        Console.WriteLine($" Результат: {Methods1.findLast(arr, x)}");
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 4:
                                Console.WriteLine(" Добавление числа x в массив arr в позицию pos");
                                while (true)
                                {
                                    Console.WriteLine(" Введите массив arr (через пробел) из целых чисел, целое число x и целое число pos:");
                                    try
                                    {
                                        int[] arr = Methods1.inputArr(Console.ReadLine());
                                        int x = int.Parse(Console.ReadLine());
                                        int pos = int.Parse(Console.ReadLine());
                                        int[] arrNew = Methods1.add(arr, x, pos);
                                        Console.WriteLine(" Результат:");
                                        Methods1.printArr(arrNew);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 6:
                                Console.WriteLine(" Реверсирование массива");
                                while (true)
                                {
                                    Console.WriteLine(" Введите массив arr (через пробел) из целых чисел:");
                                    try
                                    {
                                        int[] arr = Methods1.inputArr(Console.ReadLine());
                                        Methods1.reverse(arr);
                                        Console.WriteLine(" Результат:");
                                        Methods1.printArr(arr);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 8:
                                Console.WriteLine(" Объединение двух массивов");
                                while (true)
                                {
                                    Console.WriteLine(" Введите сначала массив arr1 (через пробел) из целых чисел, а затем arr2:");
                                    try
                                    {
                                        int[] arr1 = Methods1.inputArr(Console.ReadLine());
                                        int[] arr2 = Methods1.inputArr(Console.ReadLine());
                                        int[] arr3 = Methods1.concat(arr1, arr2);
                                        Console.WriteLine(" Результат:");
                                        Methods1.printArr(arr3);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                            case 10:
                                Console.WriteLine(" Удаление всех отрицательных чисел из массива");
                                while (true)
                                {
                                    Console.WriteLine(" Введите массив arr (через пробел) из целых чисел:");
                                    try
                                    {
                                        int[] arr = Methods1.inputArr(Console.ReadLine());
                                        int[] arrNew = Methods1.deleteNegative(arr);
                                        Console.WriteLine(" Результат:");
                                        Methods1.printArr(arrNew);
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(" Error: Invalid input");
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
        }
    }
}