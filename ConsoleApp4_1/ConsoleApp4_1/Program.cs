using System;
using System.Collections.Generic;

namespace ConsoleApp4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int task = 0;
            int n;
            int m;

            Console.WriteLine(@"
        ЛАБОРАТОРНАЯ РАБОТА №4, часть 2
       КОЛЛЕКЦИИ И ПЕРЕГРУЗКА ОПЕРАТОРОВ 
 -----------------------------------------------
    Задания №6.8, №7.8 Перегрузка операций
                 Класс Money
 -----------------------------------------------");

            Console.WriteLine("1. Тестирование конструкторов:");

            Money emptyMoney = new Money();
            Console.WriteLine($"Конструктор по умолчанию: {emptyMoney}");

            Money money1 = new Money(150, 75);
            Console.WriteLine($"Конструктор (150, 75): {money1}");

            Money money2 = new Money(34, 187);
            Console.WriteLine($"Конструктор (34, 187): {money2}");

            Money money3 = new Money(215, 24);
            Console.WriteLine($"Конструктор (215, 24): {money3}");

            Console.Write($"Конструктор (10, 150): ");
            try
            {
                Money invalidKopeks = new Money(10, 150); 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\n\n2. Тестирование метода Subtract:");

            Money result1 = money1.Subtract(money2);
            Console.WriteLine($"{money1} - {money2} = {result1}");

            Money result2 = money3.Subtract(money3);
            Console.WriteLine($"{money3} - {money3} = {result2}");

            Console.Write($"{ money2} - { money3} => ");
            try
            {
                Money result3 = money2.Subtract(money3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\n3. Тестирование унарных операций:");

            Money money4 = new Money(9, 99);
            Console.WriteLine($"Исходная сумма: {money4}");
            money4++;
            Console.WriteLine($"После ++: {money4}");

            Money money5 = new Money(20, 0);
            Console.WriteLine($"Исходная сумма: {money5}");
            money5--;
            Console.WriteLine($"После --: {money5}");

            Money money6 = new Money(0, 0);
            Console.WriteLine($"Исходная сумма: {money6}");
            Console.Write($"После --: ");

            try
            {
                money6--;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("\n4. Тестирование операций приведения:");

            uint rublesOnly = money1; 
            double kopeksOnly = (double)money1;

            Console.WriteLine($"Исходная сумма: {money1}");
            Console.WriteLine($"uint (неявная): {rublesOnly} руб.");
            Console.WriteLine($"double (явная): {kopeksOnly} руб.");

            Console.WriteLine("\n5. Тестирование бинарных операций:");

            uint num = 15;
            Console.WriteLine($"{money1} - {money2} = {money1 - money2}");
            Console.WriteLine($"{money1} - {num} руб. = {money1 - num}");
            Console.WriteLine($"{num} руб. - {money4} = {num - money4}");

            Console.ReadKey();
        }
    }
}
