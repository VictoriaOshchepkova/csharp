using System;
using System.IO;

namespace ConsoleApp3
{
    class Program
    {
        public static void Main(string[] args)
        {
            int task = 0;
            int n;
            int m;

            Console.WriteLine(@"
             ЛАБОРАТОРНАЯ РАБОТА №3
                Массивы и файлы
 -----------------------------------------------
 (1) Задание №1.8 Заполнение двумерных массивов;
 (2) Задание №2.8 Работа с двумерными массивами;
 (3) Задание №3.8 Работа с двумерными массивами как с матрицами;
 (4) Задание №4.8 Бинарные файлы;
 (5) Задание №5.8 Бинарные файлы и структуры;
 (6) Задание №6.8 Текстовые файлы;
 (7) Задание №7.8 Решить задачу с использованием структуры «текстовый файл»;
 (8) Задание №8.8 Текстовый файл.
 -----------------------------------------------");

            while (true)
            {
                while (true)
                {
                    try
                    {
                        Console.Write(" Выберите порядковый номер задания (число от 1 до 8) для исполнения: ");
                        task = int.Parse(Console.ReadLine());
                        if (task >= 1 && task <= 8)
                            break;
                        Console.WriteLine("Error: Invalid input");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: Invalid input");
                    }
                }

                switch (task)
                {
                    case 1:

                        Console.WriteLine(" №1.8 Заполнение двумерных массивов");

                        while (true)
                        {
                            try
                            {
                                Console.Write(" Введите количество строк n: ");
                                n = int.Parse(Console.ReadLine());
                                if (n > 0)
                                {
                                    break;
                                }
                                Console.WriteLine("Error: invalid input");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: invalid input");
                            }
                        }

                        while (true)
                        {
                            try
                            {
                                Console.Write(" Введите количество столбцов m: ");
                                m = int.Parse(Console.ReadLine());
                                if (m > 0)
                                {
                                    break;
                                }
                                Console.WriteLine("Error: invalid input");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: invalid input");
                            }
                        }

                        Matrix matrix1 = new Matrix(n, m);
                        Matrix matrix2 = new Matrix(n);
                        Matrix matrix3 = new Matrix(n, "special");

                        Console.WriteLine($"Созданная матрица {n}x{m}:");
                        Console.WriteLine(matrix1);

                        Console.WriteLine($"Матрица {n}x{n}, заполненная трехзначными случайными числами, составленными из возрастающих цифр:");
                        Console.WriteLine(matrix2);

                        Console.WriteLine($"Матрица {n}x{n}, заполненная зигзагом числами от 1 до {n}:");
                        Console.WriteLine(matrix3);


                        break;
                    case 2:
                        string input;
                        string[] stringArr;
                        int[] arr;

                        Console.WriteLine(" №2.8 Работа с двумерными массивами");

                        while (true)
                        {
                            try
                            {
                                Console.Write(" Введите количество элементов n в одномерном массиве: ");

                                n = int.Parse(Console.ReadLine());
                                if (n > 0)
                                {
                                    break;
                                }
                                Console.WriteLine("Error: invalid input");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: invalid input");
                            }
                        }

                        while (true)
                        {
                            try
                            {
                                Console.Write(" Введите элементы одномерного массива через пробел: ");

                                input = Console.ReadLine();
                                stringArr = input.Split(' ');
                                if (stringArr.Length == n)
                                {
                                    arr = new int[n];

                                    for (int i = 0; i < arr.Length; i++)
                                    {
                                        arr[i] = int.Parse(stringArr[i]);
                                    }
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Error: invalid input");
                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: invalid input");
                            }
                        }

                        Matrix matrix4 = new Matrix(arr);

                        Console.WriteLine($"Созданная матрица {n}x{n}:");
                        Console.WriteLine(matrix4);

                        break;
                    case 3:
                        Console.WriteLine(" №3.8 Работа с двумерными массивами как с матрицами");

                        Matrix A = new Matrix(3, "special");
                        Matrix B = new Matrix(3, "special");
                        Matrix C = new Matrix(3, "special");

                        Console.WriteLine("Матрица A:");
                        Console.WriteLine(A);
                        Console.WriteLine("Матрица B:");
                        Console.WriteLine(B);
                        Console.WriteLine("Матрица C:");
                        Console.WriteLine(C);

                        Matrix result = A - (3 * B - C.Transpose());
                        Console.WriteLine("Результат выражения A - (3 * B - C^T):");
                        Console.WriteLine(result);

                        break;
                    case 4: 
                        Console.WriteLine(" №4.8 Бинарные файлы");
                        while (true)
                        {
                            try
                            {
                                Console.Write(" Введите количество элементов n в бинарном файле: ");

                                n = int.Parse(Console.ReadLine());
                                if (n > 0)
                                {
                                    break;
                                }
                                Console.WriteLine("Error: invalid input");
                            }
                            catch
                            {
                                Console.WriteLine("Error: invalid input");
                            }
                        }

                        Files.WriteRandomNumbers(n);
                        int number;

                        try
                        {
                            using (BinaryReader reader = new BinaryReader(File.Open("task4.bin", FileMode.Open)))
                            {
                                Console.WriteLine("Данные в файле 'task4': ");
                                while (reader.BaseStream.Position < reader.BaseStream.Length)
                                {
                                    number = reader.ReadInt32();
                                    Console.Write(number + " ");
                                }
                            }
                            Console.Write("\n");
                        }
                        catch
                        {
                            Console.WriteLine("Error: invalid input");
                        }

                        Files.ReadNumbers();

                        try
                        {
                            using (BinaryReader reader = new BinaryReader(File.Open("task4fin.bin", FileMode.Open)))
                            {
                                Console.WriteLine("Данные в файле 'task4fin': ");
                                while (reader.BaseStream.Position < reader.BaseStream.Length)
                                {
                                    number = reader.ReadInt32();
                                    Console.Write(number + " ");
                                }
                            }
                            Console.Write("\n");
                        }
                        catch
                        {
                            Console.WriteLine("Error: invalid input");
                        }

                        break;
                    case 5:
                        Console.WriteLine(" №5.8 Бинарные файлы и структуры");

                        Files.CreateToysFile();
                        Files.GetToysForAges();

                        break;
                    case 6:
                        Console.WriteLine(" №6.8 Текстовые файлы");

                        while (true)
                        {
                            try
                            {
                                Console.Write(" Введите количество элементов n в текстовом файле: ");

                                n = int.Parse(Console.ReadLine());
                                if (n > 0)
                                {
                                    break;
                                }
                                Console.WriteLine("Error: invalid input");
                            }
                            catch
                            {
                                Console.WriteLine("Error: invalid input");
                            }
                        }

                        Files.FindMinMax();

                        break;
                    case 7:
                        Console.WriteLine(" №7.8 Решить задачу с использованием структуры «текстовый файл»");

                        while (true)
                        {
                            try
                            {
                                Console.Write(" Введите количество элементов n в текстовом файле: ");

                                n = int.Parse(Console.ReadLine());
                                if (n > 0)
                                {
                                    break;
                                }
                                Console.WriteLine("Error: invalid input");
                            }
                            catch
                            {
                                Console.WriteLine("Error: invalid input");
                            }
                        }

                        Files.WriteRandomNumbersTextFile(n);
                        Files.CountOddNumbers();
                        break;
                    case 8:
                        Console.WriteLine(" №8.8 Текстовый файл");

                        Files.CreateNewFileLengthString();

                        break;
                }
            }
        }
    }
}
