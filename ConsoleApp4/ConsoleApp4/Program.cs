using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class Program
    {
        public static void Main(string[] args)
        {
            int task = 0;

            Console.WriteLine(@"
         ЛАБОРАТОРНАЯ РАБОТА №4, часть 1
        КОЛЛЕКЦИИ И ПЕРЕГРУЗКА ОПЕРАТОРОВ 
 -----------------------------------------------
 (1) Задание №1.8 List;
 (2) Задание №2.8 LinkedList;
 (3) Задание №3.8 HashSet;
 (4) Задание №4.8 HashSet;
 (5) Задание №5.8 Dictionary/SortedList;
 -----------------------------------------------");

            while (true)
            {
                while (true)
                {
                    try
                    {
                        Console.Write(" \nВыберите порядковый номер задания (число от 1 до 5) для исполнения: ");
                        task = int.Parse(Console.ReadLine());
                        if (task >= 1 && task <= 5)
                            break;
                        Console.WriteLine("Error: Invalid input");
                    }
                    catch
                    {
                        Console.WriteLine("Error: Invalid input");
                    }
                }

                switch (task)
                {
                    case 1:
                        List<object>  L1 = new List<object>() { 1, "flower", "%", -2.4, "Война и мир", "%" };
                        List<object>  L2 = new List<object>() { "A", "*", "table", 1.32 };
                        
                        Console.WriteLine("Список L1:");
                        Console.WriteLine(string.Join(", ", L1)); ;
                        Console.WriteLine("\nСписок L2:");
                        Console.WriteLine(string.Join(", ", L2));

                        object elem = "%";
                        Tasks1_5.InsertAfterFirstE(L1, elem, L2);
                        Console.WriteLine($"\nСписок L1 после вставки списка L2 после первого вхождения элемента {elem}:");
                        Console.WriteLine(string.Join(", ", L1));

                        elem = "^";
                        Console.WriteLine($"\nРезультат попытки вставки списка L2 в список L1 после первого вхождения элемента {elem}:");

                        try
                        {
                            Tasks1_5.InsertAfterFirstE(L1, elem, L2);
                            Console.WriteLine(string.Join(", ", L1));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    case 2:
                        LinkedList<object> LL1 = new LinkedList<object>();
                        LL1.AddLast("B");
                        LL1.AddLast(23);
                        LL1.AddLast("(");
                        LL1.AddLast("Солнце");

                        Console.WriteLine("Двунаправленный писок LL1:");
                        Console.WriteLine(string.Join(", ", LL1));

                        elem = "^";
                        Tasks1_5.InsertEBeginningAndEnd(LL1, elem);
                        Console.WriteLine($"\nДвунаправленный список LL1 после вставки {elem} в начало и конец:");
                        Console.WriteLine(string.Join(", ", LL1));

                        break;
                    case 3:
                        HashSet<string> books = new HashSet<string> { "Мастер и Маргарита", "Гордость и предубеждение", "Преступление и наказание", "1984" };
                        HashSet<string> reader1 = new HashSet<string> { "Мастер и Маргарита", "1984", "Преступление и наказание" };
                        HashSet<string> reader2 = new HashSet<string> { "Преступление и наказание", "1984" };
                        HashSet<string> reader3 = new HashSet<string> { "Мастер и Маргарита", "1984"};
                        HashSet<HashSet<string>> readers = new HashSet<HashSet<string>> { reader1, reader2, reader3 };

                        Console.WriteLine("Перечень всех книг:");
                        Console.WriteLine(string.Join(", ", books));
                        Console.WriteLine("\nПеречень книг, прочитанных читателем №1:");
                        Console.WriteLine(string.Join(", ", reader1));
                        Console.WriteLine("\nПеречень книг, прочитанных читателем №2:");
                        Console.WriteLine(string.Join(", ", reader2));
                        Console.WriteLine("\nПеречень книг, прочитанных читателем №3:");
                        Console.WriteLine(string.Join(", ", reader3));

                        Tasks1_5.AnalyzeBooks(books, readers);

                        break;
                    case 4:
                        Tasks1_5.PrintCharText("task4.txt");

                        break;
                    case 5:
                        Tasks1_5.FillDataFile("task5.txt", 12);
                        Tasks1_5.OlympiadTask("task5.txt");
                        break;
                }
            }
        }
    }
}
