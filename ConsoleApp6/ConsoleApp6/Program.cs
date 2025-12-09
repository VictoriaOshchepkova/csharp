using System;

namespace ConsoleApp6
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.Write(
@"       ЛАБОРАТОРНАЯ РАБОТА №6
      Интерфейсы и шаблоны ООП
-----------------------------------
1. Задание №1. Кот
2. Задание №2. Дроби
0. Выход
-----------------------------------
Выберите пункт: ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Задание №1. Кот");
                            Console.WriteLine("-----------------------------------");

                            Cat barsik = new Cat("Барсик");

                            Console.WriteLine(barsik);
                            barsik.Meow();
                            barsik.Meow(3);
                            Console.WriteLine();

                            Cat batman = new Cat("Бэтмен");
                            Parrot sema = new Parrot("Сёма");
                            Parrot gosha = new Parrot("Гоша");

                            Funs.MeowsCare(barsik, batman, batman, sema, gosha, barsik, barsik);
                            break;
                        case "2":
                            Console.Clear();
                            Console.WriteLine("Задание №2. Дроби");
                            Console.WriteLine("-----------------------------------");

                            Fraction f1 = new Fraction(1, 2);
                            Fraction f2 = new Fraction(2, 3);
                            Fraction f3 = new Fraction(3, 4);
                            Fraction f4 = new Fraction(4, 5);

                            Console.WriteLine("Созданные дроби:");
                            Console.WriteLine($"f1 = {f1}");
                            Console.WriteLine($"f2 = {f2}");
                            Console.WriteLine($"f3 = {f3}");
                            Console.WriteLine($"f4 = {f4}");

                            Console.WriteLine("\nПримеры операций:");

                            Console.WriteLine($"{f1} + {f2} = {f1 + f2}");
                            Console.WriteLine($"{f1} + {5} = {f1 + 5}");
                            Console.WriteLine($"{f3} - {f4} = {f3 - f4}");
                            Console.WriteLine($"{4} - {f4} = {4 - f4}");
                            Console.WriteLine($"{f1} * {f2} = {f1 * f2}");
                            Console.WriteLine($"{f1} * (-4) = {f1 * -4}");
                            Console.WriteLine($"{f3} / {f4} = {f3 / f4}");
                            Console.WriteLine($"{f3} / {9} = {f3 / 9}");

                            Console.WriteLine($"\nВычисление f1 + f2 / f3 - 5:");
                            Console.WriteLine($"{f1} + {f2} / {f3} - 5 = {f1 + f2 / f3 - 5}");

                            Console.WriteLine($"\nСравнение дробей:");
                            Console.WriteLine($"{f2} == {f4}: {f2.Equals(f4)}");

                            Fraction f5 = new Fraction(4, 5);
                            Console.WriteLine($"{f4} == {f5}: {f4.Equals(f5)}");

                            Console.WriteLine($"\nКлонирование дроби:");
                            Fraction f6 = (Fraction)f2.Clone();
                            Console.WriteLine($"Клон {f2}: {f6}");

                            Console.WriteLine($"\nПолучение вещественного значения дроби:");
                            Console.WriteLine($"Вещественное значение {f1}: {f1.GetRealValue()}");
                            Console.WriteLine($"Вещественное значение {f2}: {f2.GetRealValue()}");

                            Console.WriteLine($"\nУстановка числителя и знаменателя дроби:");

                            Console.Write($"Обновление значений дроби {f1} на -5 и 0: ");
                            try
                            {
                                f1.SetValues(-5, 0);
                                Console.WriteLine(f1);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            Console.Write($"Обновление значений дроби {f2} на 36 и -24: ");
                            f2.SetValues(36, -24);
                            Console.WriteLine(f2);

                            Console.WriteLine($"\nКеширование вещественного значения дроби:");

                            Console.WriteLine($"Дробь: {f1}"); 
                            Console.WriteLine($"Вещественное значение: {f1.GetRealValue()}");
                            f1.SetValues(3, 4);
                            Console.WriteLine($"Обновленное значение дроби: {f1}");
                            Console.WriteLine($"Новое вещественное значение: {f1.GetRealValue()}");
                            Console.WriteLine($"Повторный вызов (кэш): {f1.GetRealValue()}");
                            break;
                        case "0":
                            Console.WriteLine("Выход из программы...");
                            return;
                        default:
                            Console.WriteLine("Такого пункта нет. Выберите число от 0 до 2.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}
