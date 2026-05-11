namespace ConsoleApp2_1
{
    /// <summary>
    /// Главный класс программы, содержащий точку входа.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine(@"
             ЛАБОРАТОРНАЯ РАБОТА №2
                      ООП
 (1) Задание №1.3 Имена;
 (2) Задание №1.5 Дом;
 (3) Задание №2.4 Сотрудники и отделы;
 (4) Задание №3.4 Сотрудники и отделы;
 (5) Задание №4.5 Создаем имена;
 (6) Задание №5.5 Дроби.
");

            while (true)
            {
                int task = GetUserTaskChoice(); // Принцип единой ответственности

                switch (task)
                {
                    case 1:
                        RunNameTask();
                        break;
                    case 2:
                        RunHouseTask();
                        break;
                    case 3:
                        RunEmployeeTask();
                        break;
                    case 4:
                        RunEmployeeExpandedTask();
                        break;
                    case 5:
                        RunConstrainedNameTask();
                        break;
                    case 6:
                        RunFractionTask();
                        break;
                    default:
                        Console.WriteLine("Error: Invalid task number.");
                        break;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Получает от пользователя номер задания для выполнения.
        /// </summary>
        /// <returns>Число от 1 до 6, соответствующее выбранному заданию.</returns>
        private static int GetUserTaskChoice()
        {
            while (true)
            {
                Console.WriteLine("Выберите порядковый номер задания (число от 1 до 6) для исполнения:");

                try
                {
                    int task = int.Parse(Console.ReadLine() ?? string.Empty);

                    if (task >= 1 && task <= 6)
                    {
                        return task;
                    }

                    Console.WriteLine("Error: The number must be between 1 and 6.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: The input data must be an integer.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Error: The number is too large or too small.");
                }
            }
        }

        /// <summary>
        /// Выполняет задание №1.3 "Имена".
        /// Демонстрирует создание объектов Name с различными комбинациями параметров,
        /// а также позволяет пользователю ввести свои данные для создания имени.
        /// </summary>
        private static void RunNameTask()
        {
            Console.WriteLine("№1.3 Создание сущности Имя из имени, фамилии и/или отчества");

            Name name1 = new Name(null, "Клеопатра", null);
            Name name2 = new Name("Пушкин", "Александр", "Сергеевич");
            Name name3 = new Name("Маяковский", "Владимир", null);

            Console.WriteLine("Примеры:");
            Console.WriteLine(name1);
            Console.WriteLine(name2);
            Console.WriteLine(name3);

            Console.WriteLine("\nВведите фамилию:");
            string? lastName = Console.ReadLine()?.Trim();

            Console.WriteLine("Введите имя:");
            string? firstName = Console.ReadLine()?.Trim();

            Console.WriteLine("Введите отчество:");
            string? patronymic = Console.ReadLine()?.Trim();

            try
            {
                Name newName = new Name(lastName, firstName, patronymic);
                Console.WriteLine($"Результат: {newName}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Выполняет задание №1.5 "Дом".
        /// Демонстрирует создание объектов House с различным количеством этажей,
        /// а также позволяет пользователю ввести своё значение для создания дома.
        /// </summary>
        private static void RunHouseTask()
        {
            Console.WriteLine("№1.5 Создание сущности Дом с N этажами");

            House house1 = new House(1);
            House house2 = new House(5);
            House house3 = new House(23);

            Console.WriteLine("Примеры:");
            Console.WriteLine(house1);
            Console.WriteLine(house2);
            Console.WriteLine(house3);

            while (true)
            {
                Console.WriteLine("\nВведите натуральное число N (количество этажей):");

                try
                {
                    int n = int.Parse(Console.ReadLine() ?? string.Empty);
                    House newHouse = new House(n);
                    Console.WriteLine($"Результат: {newHouse}");
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Enter an integer.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Error: The number is too large or too small.");
                }
            }
        }

        /// <summary>
        /// Выполняет задание №2.4 "Сотрудники и отделы".
        /// Демонстрирует создание сотрудников и отдела, назначение руководителя,
        /// а также позволяет пользователю создать свой отдел с сотрудниками.
        /// </summary>
        private static void RunEmployeeTask()
        {
            Console.WriteLine("№2.4 Создание сущности Сотрудник");

            Department itDepartment = new Department("IT");
            Employee employee1 = new Employee("Петров", itDepartment);
            Employee employee2 = new Employee("Козлов", itDepartment);
            Employee employee3 = new Employee("Сидоров", itDepartment);
            itDepartment.Manager = employee2;

            Console.WriteLine("Пример:");
            Console.WriteLine(employee1);
            Console.WriteLine(employee2);
            Console.WriteLine(employee3);

            string departmentName;

            while (true)
            {
                Console.Write("\nВведите название отдела: ");
                departmentName = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(departmentName))
                {
                    break;
                }

                Console.WriteLine("Error: Department name cannot be empty.");
            }

            Department department = new Department(departmentName);
            int count;

            while (true)
            {
                Console.Write("Введите количество сотрудников: ");

                if (int.TryParse(Console.ReadLine(), out count) && count > 0)
                {
                    break;
                }

                Console.WriteLine("Error: Enter a positive integer.");
            }

            List<Employee> employees = new List<Employee>();

            for (int i = 0; i < count; i++)
            {
                string employeeName;

                while (true)
                {
                    Console.Write($"Имя сотрудника {i + 1}: ");
                    employeeName = Console.ReadLine() ?? string.Empty;

                    if (!string.IsNullOrWhiteSpace(employeeName))
                    {
                        break;
                    }

                    Console.WriteLine("Error: Employee name cannot be empty.");
                }

                employees.Add(new Employee(employeeName, department));
            }

            Console.WriteLine("Список сотрудников:");

            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {employees[i].Name}");
            }

            int managerIndex;

            while (true)
            {
                Console.Write("Выберите номер начальника отдела: ");

                if (int.TryParse(Console.ReadLine(), out managerIndex) && managerIndex >= 1 && managerIndex <= employees.Count)
                {
                    break;
                }

                Console.WriteLine($"Error: Enter a number between 1 and {employees.Count}.");
            }

            department.Manager = employees[managerIndex - 1];

            Console.WriteLine("\nРезультат:");

            foreach (Employee emp in employees)
            {
                Console.WriteLine(emp);
            }
        }

        /// <summary>
        /// Выполняет задание №3.4 "Сотрудники и отделы (расширенная версия)".
        /// Демонстрирует создание сотрудников, которые автоматически добавляются в отдел,
        /// возможность просмотра всех сотрудников отдела, а также позволяет пользователю
        /// создать несколько отделов и искать сотрудников по имени.
        /// </summary>
        private static void RunEmployeeExpandedTask()
        {
            Console.WriteLine("№3.4 Создание сущности Сотрудник с выводом всего отдела");

            DepartmentExpanded itDepartmentEx = new DepartmentExpanded("IT");
            EmployeeExpanded employeeEx1 = new EmployeeExpanded("Петров", itDepartmentEx);
            EmployeeExpanded employeeEx2 = new EmployeeExpanded("Козлов", itDepartmentEx);
            EmployeeExpanded employeeEx3 = new EmployeeExpanded("Сидоров", itDepartmentEx);
            itDepartmentEx.Manager = employeeEx2;

            Console.WriteLine("Пример:");
            Console.WriteLine(employeeEx1);
            Console.WriteLine(employeeEx2);
            Console.WriteLine(employeeEx3);

            Console.WriteLine($"Список всех сотрудников отдела {employeeEx1.Department.Name}, в котором работает {employeeEx1.Name}:");

            foreach (EmployeeExpanded emp in employeeEx1.Department.Employees)
            {
                Console.WriteLine($"- {emp.Name}");
            }

            int departmentCount;

            while (true) // Ввод с клавиатуры
            {
                Console.Write("\nВведите количество отделов: ");

                if (int.TryParse(Console.ReadLine(), out departmentCount) && departmentCount > 0)
                {
                    break;
                }

                Console.WriteLine("Error: Enter a positive integer.");
            }

            List<DepartmentExpanded> departments = new List<DepartmentExpanded>();
            List<EmployeeExpanded> allEmployees = new List<EmployeeExpanded>();

            for (int d = 0; d < departmentCount; d++)
            {
                Console.WriteLine($"\nОтдел {d + 1}.");

                string departmentName;

                while (true)
                {
                    Console.Write("Введите название отдела: ");
                    departmentName = Console.ReadLine() ?? string.Empty;

                    if (!string.IsNullOrWhiteSpace(departmentName))
                    {
                        break;
                    }

                    Console.WriteLine("Error: Department name cannot be empty.");
                }

                DepartmentExpanded department = new DepartmentExpanded(departmentName);
                departments.Add(department);

                int count;

                while (true)
                {
                    Console.Write("Введите количество сотрудников: ");

                    if (int.TryParse(Console.ReadLine(), out count) && count > 0)
                    {
                        break;
                    }

                    Console.WriteLine("Error: Enter a positive integer.");
                }

                List<EmployeeExpanded> departmentEmployees = new List<EmployeeExpanded>();

                for (int i = 0; i < count; i++)
                {
                    string employeeName;

                    while (true)
                    {
                        Console.Write($"Имя сотрудника {i + 1}: ");
                        employeeName = Console.ReadLine() ?? string.Empty;

                        if (!string.IsNullOrWhiteSpace(employeeName))
                        {
                            break;
                        }

                        Console.WriteLine("Error: Employee name cannot be empty.");
                    }

                    EmployeeExpanded employee = new EmployeeExpanded(employeeName, department);
                    departmentEmployees.Add(employee);
                    allEmployees.Add(employee);
                }

                int managerIndex;

                while (true)
                {
                    Console.Write("Выберите номер начальника отдела: ");

                    if (int.TryParse(Console.ReadLine(), out managerIndex) && managerIndex >= 1 && managerIndex <= departmentEmployees.Count)
                    {
                        break;
                    }

                    Console.WriteLine($"Error: Enter a number between 1 and {departmentEmployees.Count}.");
                }

                department.Manager = departmentEmployees[managerIndex - 1];
            }

            while (true)
            {
                Console.Write("\nВведите имя сотрудника для просмотра его отдела: ");
                string searchName = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(searchName))
                {
                    Console.WriteLine("Error: Name cannot be empty.");
                    continue;
                }

                EmployeeExpanded? foundEmployee = allEmployees.Find(e => e.Name == searchName);

                if (foundEmployee == null)
                {
                    Console.WriteLine($"Сотрудник '{searchName}' не найден.");
                    continue;
                }

                Console.WriteLine($"\n{foundEmployee}");
                Console.WriteLine($"Список всех сотрудников отдела {foundEmployee.Department.Name}:");

                foreach (EmployeeExpanded emp in foundEmployee.Department.Employees)
                {
                    Console.WriteLine($"- {emp.Name}");
                }
            }
        }

        /// <summary>
        /// Выполняет задание №4.5 "Создаем имена".
        /// Демонстрирует создание объектов NameConstrained с различными комбинациями
        /// обязательных и необязательных параметров, а также позволяет пользователю
        /// ввести свои данные для создания имени с ограничениями.
        /// </summary>
        private static void RunConstrainedNameTask()
        {
            Console.WriteLine("№4.5 Создание сущности Имя из имени, имени и фамилии или ФИО");

            NameConstrained nameCnst1 = new NameConstrained("Клеопатра");
            NameConstrained nameCnst2 = new NameConstrained("Александр", "Пушкин", "Сергеевич");
            NameConstrained nameCnst3 = new NameConstrained("Владимир", "Маяковский");
            NameConstrained nameCnst4 = new NameConstrained("Христофор", "Бонифатьевич");

            Console.WriteLine("Примеры:");
            Console.WriteLine(nameCnst1);
            Console.WriteLine(nameCnst2);
            Console.WriteLine(nameCnst3);
            Console.WriteLine(nameCnst4);

            Console.WriteLine("\nВведите данные для создания имени:");

            string firstNameCnst;

            while (true)
            {
                Console.Write("Введите имя (обязательно): ");
                firstNameCnst = Console.ReadLine()?.Trim() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(firstNameCnst))
                {
                    break;
                }

                Console.WriteLine("Error: Name is required.");
            }

            Console.Write("Введите фамилию (необязательно): ");
            string lastNameCnst = Console.ReadLine()?.Trim() ?? string.Empty;

            Console.Write("Введите отчество (необязательно): ");
            string patronymicCnst = Console.ReadLine()?.Trim() ?? string.Empty;

            try
            {
                NameConstrained newNameCnst;

                if (!string.IsNullOrEmpty(firstNameCnst) && !string.IsNullOrEmpty(lastNameCnst) && !string.IsNullOrEmpty(patronymicCnst))
                {
                    newNameCnst = new NameConstrained(firstNameCnst, lastNameCnst, patronymicCnst);
                    Console.WriteLine($"Результат: {newNameCnst}");
                }
                else if (!string.IsNullOrEmpty(firstNameCnst) && !string.IsNullOrEmpty(lastNameCnst))
                {
                    newNameCnst = new NameConstrained(lastNameCnst, firstNameCnst);
                    Console.WriteLine($"Результат: {newNameCnst}");
                }
                else if (!string.IsNullOrEmpty(firstNameCnst))
                {
                    newNameCnst = new NameConstrained(firstNameCnst);
                    Console.WriteLine($"Результат: {newNameCnst}");
                }
                else
                {
                    Console.WriteLine("Error: Invalid input.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Выполняет задание №5.5 "Дроби".
        /// Демонстрирует создание дробей, выполнение арифметических операций
        /// (сложение, вычитание, умножение, деление) с дробями и целыми числами,
        /// а также позволяет пользователю ввести свои дроби для выполнения операций.
        /// </summary>
        private static void RunFractionTask()
        {
            Console.WriteLine("№5.5 Работа с сущностью Дробь");

            Fraction f1 = new Fraction(1, 2);
            Fraction f2 = new Fraction(2, 3);
            Fraction f3 = new Fraction(3, 4);
            Fraction f4 = new Fraction(4, 5);

            Console.WriteLine("Примеры готовых дробей:");
            Console.WriteLine($"f1 = {f1}");
            Console.WriteLine($"f2 = {f2}");
            Console.WriteLine($"f3 = {f3}");
            Console.WriteLine($"f4 = {f4}");

            Console.WriteLine("\nПримеры операций с готовыми дробями:");
            Console.WriteLine($"{f1} + {f2} = {f1.Sum(f2)}");
            Console.WriteLine($"{f3} - {f4} = {f3.Minus(f4)}");
            Console.WriteLine($"{f1} * {f2} = {f1.Multiply(f2)}");
            Console.WriteLine($"{f3} / {f4} = {f3.Div(f4)}");
            Console.WriteLine($"{f1} + 2 = {f1.Sum(2)}");
            Console.WriteLine($"{f3} - 1 = {f3.Minus(1)}");
            Console.WriteLine($"{f2} * 3 = {f2.Multiply(3)}");
            Console.WriteLine($"{f4} / 2 = {f4.Div(2)}");

            Console.WriteLine($"\n({f1}).Sum({f2}).Div({f3}).Minus(5) = {f1.Sum(f2).Div(f3).Minus(5)}");

            try
            {
                Console.WriteLine("\nВведите первую дробь:");
                Fraction fracA = ReadFractionFromConsole("A");

                Console.WriteLine("\nВведите вторую дробь:");
                Fraction fracB = ReadFractionFromConsole("B");

                Console.WriteLine("\nВыберите операцию:");
                Console.WriteLine("(1) Сложение");
                Console.WriteLine("(2) Вычитание");
                Console.WriteLine("(3) Умножение");
                Console.WriteLine("(4) Деление");
                Console.Write("Введите номер операции: ");

                int operation;

                while (!int.TryParse(Console.ReadLine(), out operation) || operation < 1 || operation > 4)
                {
                    Console.WriteLine("Error: Enter a number between 1 and 4.");
                    Console.Write("Введите номер операции: ");
                }

                switch (operation)
                {
                    case 1:
                        Console.WriteLine($"\n{fracA} + {fracB} = {fracA.Sum(fracB)}");
                        break;
                    case 2:
                        Console.WriteLine($"\n{fracA} - {fracB} = {fracA.Minus(fracB)}");
                        break;
                    case 3:
                        Console.WriteLine($"\n{fracA} * {fracB} = {fracA.Multiply(fracB)}");
                        break;
                    case 4:
                        Console.WriteLine($"\n{fracA} / {fracB} = {fracA.Div(fracB)}");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Считывает дробь с консоли, запрашивая у пользователя числитель и знаменатель.
        /// Выполняет валидацию ввода и повторяет запрос при некорректных данных.
        /// </summary>
        /// <param name="fractionName">Имя дроби для отображения в подсказках (например, "A" или "B").</param>
        /// <returns>Объект Fraction, созданный на основе введенных пользователем данных.</returns>
        private static Fraction ReadFractionFromConsole(string fractionName)
        {
            int numerator;
            int denominator;

            while (true)
            {
                Console.Write($"Введите числитель дроби {fractionName}: ");

                if (!int.TryParse(Console.ReadLine(), out numerator))
                {
                    Console.WriteLine("Error: Numerator must be an integer.");
                    continue;
                }

                Console.Write($"Введите знаменатель дроби {fractionName}: ");

                if (!int.TryParse(Console.ReadLine(), out denominator))
                {
                    Console.WriteLine("Error: Denominator must be an integer.");
                    continue;
                }

                try
                {
                    Fraction fraction = new Fraction(numerator, denominator);
                    Console.WriteLine($"Дробь {fractionName} = {fraction}");
                    return fraction;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}