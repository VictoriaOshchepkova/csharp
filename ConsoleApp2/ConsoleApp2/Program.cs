using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        public static void Main(string[] args)
        {
            int task = 0;

            Console.WriteLine(@"
             ЛАБОРАТОРНАЯ РАБОТА №2
                      ООП
 -----------------------------------------------
 (1) Задание №1.3 Имена;
 (2) Задание №1.5 Дом;
 (3) Задание №2.4 Сотрудники и отделы;
 (4) Задание №3.4 Сотрудники и отделы;
 (5) Задание №4.5 Создаем имена;
 (6) Задание №5.5 Дроби.
 -----------------------------------------------");

            while (true)
            {
                while (true)
                {
                    Console.WriteLine(" Выберите порядковый номер задания (число от 1 до 6) для исполнения:");

                    try
                    {
                        task = int.Parse(Console.ReadLine());
                        if (task >= 1 && task <= 6)
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
                        Console.WriteLine(" №1.3 Создание сущности Имя из имени, фамилии и/или отчества");

                        Name name1 = new Name(null, "Клеопатра", null);
                        Name name2 = new Name("Пушкин", "Александр", "Сергеевич");
                        Name name3 = new Name("Маяковский", "Владимир", null);

                        Console.WriteLine(" Примеры:");
                        Console.WriteLine(name1);
                        Console.WriteLine(name2);
                        Console.WriteLine(name3);

                        Console.WriteLine(" Введите фамилию:");
                        string lastName = Console.ReadLine();

                        Console.WriteLine(" Введите имя:");
                        string firstName = Console.ReadLine();

                        Console.WriteLine(" Введите отчество:");
                        string patronymic = Console.ReadLine();

                        try
                        {
                            Name newName = new Name(lastName, firstName, patronymic);
                            Console.WriteLine($" Результат: {newName}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        Console.WriteLine(" №1.5 Создание сущности Дом с N этажами");

                        House house1 = new House(1);
                        House house2 = new House(5);
                        House house3 = new House(23);

                        Console.WriteLine(" Пример:");
                        Console.WriteLine(house1);
                        Console.WriteLine(house2);
                        Console.WriteLine(house3);

                        int n;
                        while (true)
                        {
                            Console.WriteLine(" Введите натуральное число N:");
                            try
                            {
                                n = int.Parse(Console.ReadLine());
                                House newHouse = new House(n);
                                Console.WriteLine($" Результат: {newHouse}");
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: Invalid input");
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine(" №2.4 Создание сущности Сотрудник");

                        Department itDepartment = new Department("IT");
                        Employee employee1 = new Employee("Петров", itDepartment);
                        Employee employee2 = new Employee("Козлов", itDepartment);
                        Employee employee3 = new Employee("Сидоров", itDepartment);
                        itDepartment.Manager = employee2;

                        Console.WriteLine(employee1);
                        Console.WriteLine(employee2);
                        Console.WriteLine(employee3);
                        break;
                    case 4:
                        Console.WriteLine(" №3.4 Создание сущности Сотрудник с выводом всего отдела");

                        DepartmentExpanded itDepartmentEx = new DepartmentExpanded("IT");
                        EmployeeExpanded employeeEx1 = new EmployeeExpanded("Петров", itDepartmentEx);
                        EmployeeExpanded employeeEx2 = new EmployeeExpanded("Козлов", itDepartmentEx);
                        EmployeeExpanded employeeEx3 = new EmployeeExpanded("Сидоров", itDepartmentEx);
                        itDepartmentEx.Manager = employeeEx2;

                        Console.WriteLine(employeeEx1);
                        Console.WriteLine(employeeEx2);
                        Console.WriteLine(employeeEx3);

                        Console.WriteLine($"Список всех сотрудников отдела {employeeEx1.Department.Name}, в котором работает {employeeEx1.Name}:");
                        for (int i = 0; i < employeeEx1.Department.EmployeeCount; i++)
                        {
                            Console.WriteLine($"- {employeeEx1.Department.Employees[i].Name}");
                        }
                        break;
                    case 5:
                        Console.WriteLine(" №4.5 Создание сущности Имя из имени, имени и фамилии или ФИО");

                        NameСonstrained nameCnst1 = new NameСonstrained("Клеопатра");
                        NameСonstrained nameCnst2 = new NameСonstrained("Александр", "Пушкин", "Сергеевич");
                        NameСonstrained nameCnst3 = new NameСonstrained("Владимир", "Маяковский");
                        NameСonstrained nameCnst4 = new NameСonstrained("Христофор", "Бонифатьевич");

                        Console.WriteLine(" Примеры:");
                        Console.WriteLine(nameCnst1);
                        Console.WriteLine(nameCnst2);
                        Console.WriteLine(nameCnst3);
                        Console.WriteLine(nameCnst4);

                        Console.WriteLine(" Введите имя:");
                        string firstNameCnst = Console.ReadLine();

                        Console.WriteLine(" Введите фамилию:");

                        string lastNameCnst = Console.ReadLine();

                        Console.WriteLine(" Введите отчество:");
                        string patronymicCnst = Console.ReadLine();

                        try
                        {
                            NameСonstrained newNameCnst;

                            if (!string.IsNullOrEmpty(firstNameCnst) && !string.IsNullOrEmpty(lastNameCnst) && !string.IsNullOrEmpty(patronymicCnst))
                            {
                                newNameCnst = new NameСonstrained(firstNameCnst, lastNameCnst, patronymicCnst);
                                Console.WriteLine($" Результат: {newNameCnst}");
                            }
                            else if (!string.IsNullOrEmpty(firstNameCnst) && !string.IsNullOrEmpty(lastNameCnst))
                            {
                                newNameCnst = new NameСonstrained(lastNameCnst, firstNameCnst);
                                Console.WriteLine($" Результат: {newNameCnst}");
                            }
                            else if (!string.IsNullOrEmpty(firstNameCnst) && string.IsNullOrEmpty(patronymicCnst))
                            {
                                newNameCnst = new NameСonstrained(firstNameCnst);
                                Console.WriteLine($" Результат: {newNameCnst}");
                            }
                            else
                            {
                                Console.WriteLine("Error: Invalid input");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 6:
                        Console.WriteLine(" №5.5 Работа с сущностью Дробь");

                        Fraction f1 = new Fraction(1, 2);
                        Fraction f2 = new Fraction(2, 3);
                        Fraction f3 = new Fraction(3, 4);
                        Fraction f4 = new Fraction(4, 5);

                        Console.WriteLine(" Созданные дроби:");
                        Console.WriteLine($"f1 = {f1}");
                        Console.WriteLine($"f2 = {f2}");
                        Console.WriteLine($"f3 = {f3}");
                        Console.WriteLine($"f4 = {f4}"); 
                        
                        Console.WriteLine(" Примеры операций:");

                        Fraction result2 = f1.Sum(f2);
                        Console.WriteLine($"{f1} + {f2} = {result2}");

                        Fraction result3 = f3.Minus(f4);
                        Console.WriteLine($"{f3} - {f4} = {result3}");

                        Fraction result1 = f1.Multiply(f2);
                        Console.WriteLine($"{f1} * {f2} = {result1}");

                        Fraction result4 = f3.Div(f4);
                        Console.WriteLine($"{f3} / {f4} = {result4}");

                        Fraction result5 = f1.Sum(2);
                        Console.WriteLine($"{f1} + 2 = {result5}");

                        Fraction result7 = f3.Minus(1);
                        Console.WriteLine($"{f3} - 1 = {result7}");

                        Fraction result6 = f2.Multiply(3);
                        Console.WriteLine($"{f2} * 3 = {result6}");

                        Fraction result8 = f4.Div(2);
                        Console.WriteLine($"{f4} / 2 = {result8}");

                        Console.WriteLine(" Вычисление f1.Sum(f2).Div(f3).Minus(5):");
                        Fraction result9 = f1.Sum(f2).Div(f3).Minus(5);
                        Console.WriteLine($"({f1}).Sum({f2}).Div({f3}).Minus(5) = {result9}");
                        break;
                }
            }
        }
    }
}
