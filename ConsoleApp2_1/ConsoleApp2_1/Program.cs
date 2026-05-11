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
                int task = GetUserTaskChoice();

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
        /// </summary>
        private static void RunNameTask()
        {
            Console.WriteLine("№1.3 Создание сущности Имя");

            CreateExamplesNameTask();
            InputNameTask();
        }

        /// <summary>
        /// Создает примеры объектов Name для демонстрации.
        /// </summary>
        private static void CreateExamplesNameTask()
        {
            Name name1 = new Name(null, "Клеопатра", null);
            Name name2 = new Name("Пушкин", "Александр", "Сергеевич");
            Name name3 = new Name("Маяковский", "Владимир", null);

            Console.WriteLine("Примеры:");
            Console.WriteLine(name1);
            Console.WriteLine(name2);
            Console.WriteLine(name3);
        }

        /// <summary>
        /// Запрашивает у пользователя данные для создания объекта Name.
        /// </summary>
        private static void InputNameTask()
        {
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
        /// </summary>
        private static void RunHouseTask()
        {
            Console.WriteLine("№1.5 Создание сущности Дом");

            CreateExamplesHouseTask();
            InputHouseTask();
        }

        /// <summary>
        /// Создает примеры объектов House для демонстрации.
        /// </summary>
        private static void CreateExamplesHouseTask()
        {
            House house1 = new House(1);
            House house2 = new House(5);
            House house3 = new House(23);

            Console.WriteLine("\nПримеры:");
            Console.WriteLine(house1);
            Console.WriteLine(house2);
            Console.WriteLine(house3);
        }

        /// <summary>
        /// Запрашивает у пользователя данные для создания объекта House.
        /// </summary>
        private static void InputHouseTask()
        {
            while (true)
            {
                Console.WriteLine("\nВведите количество этажей:");

                try
                {
                    int floors = int.Parse(Console.ReadLine() ?? string.Empty);

                    House house = new House(floors);

                    Console.WriteLine($"Результат: {house}");
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Enter integer.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        /// Выполняет задание №2.4 "Сотрудники и отделы".
        /// </summary>
        private static void RunEmployeeTask()
        {
            Console.WriteLine("№2.4 Сотрудники и отделы");

            CreateExamplesEmployeeTask();
            InputEmployeeTask();
        }

        /// <summary>
        /// Создает примеры объектов Employee и Department для демонстрации.
        /// </summary>
        private static void CreateExamplesEmployeeTask()
        {
            Department itDepartment = new Department("IT");

            Employee employee1 = new Employee("Петров", itDepartment);
            Employee employee2 = new Employee("Козлов", itDepartment);
            Employee employee3 = new Employee("Сидоров", itDepartment);

            itDepartment.Manager = employee2;

            Console.WriteLine("\nПример:");
            Console.WriteLine(employee1);
            Console.WriteLine(employee2);
            Console.WriteLine(employee3);
        }

        /// <summary>
        /// Запрашивает у пользователя данные для создания сотрудников и отделов.
        /// </summary>
        private static void InputEmployeeTask()
        {
            string departmentName;

            while (true)
            {
                Console.Write("\nВведите название отдела: ");

                departmentName = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(departmentName))
                {
                    break;
                }

                Console.WriteLine("Error: Empty name.");
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

                Console.WriteLine("Error: Enter positive integer.");
            }

            List<Employee> employees = new List<Employee>();

            for (int i = 0; i < count; i++)
            {
                string employeeName;

                while (true)
                {
                    Console.Write($"Введите имя сотрудника {i + 1}: ");

                    employeeName = Console.ReadLine() ?? string.Empty;

                    if (!string.IsNullOrWhiteSpace(employeeName))
                    {
                        break;
                    }

                    Console.WriteLine("Error: Empty name.");
                }

                employees.Add(new Employee(employeeName, department));
            }

            Console.WriteLine("\nСотрудники:");

            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {employees[i].Name}");
            }

            int managerIndex;

            while (true)
            {
                Console.Write("Введите номер начальника: ");

                if (int.TryParse(Console.ReadLine(), out managerIndex)
                    && managerIndex >= 1
                    && managerIndex <= employees.Count)
                {
                    break;
                }

                Console.WriteLine("Error: Invalid number.");
            }

            department.Manager = employees[managerIndex - 1];

            Console.WriteLine("\nРезультат:");

            foreach (Employee employee in employees)
            {
                Console.WriteLine(employee);
            }
        }

        /// <summary>
        /// Выполняет задание №3.4 "Сотрудники и отделы (расширенная версия)".
        /// </summary>
        private static void RunEmployeeExpandedTask()
        {
            Console.WriteLine("№3.4 Расширенные сотрудники и отделы");

            CreateExamplesEmployeeExpandedTask();
            InputEmployeeExpandedTask();
        }

        /// <summary>
        /// Создает примеры объектов EmployeeExpanded и DepartmentExpanded для демонстрации.
        /// </summary>
        private static void CreateExamplesEmployeeExpandedTask()
        {
            DepartmentExpanded department = new DepartmentExpanded("IT");

            EmployeeExpanded employee1 = new EmployeeExpanded("Петров", department);
            EmployeeExpanded employee2 = new EmployeeExpanded("Козлов", department);
            EmployeeExpanded employee3 = new EmployeeExpanded("Сидоров", department);

            department.Manager = employee2;

            Console.WriteLine("\nПример:");

            Console.WriteLine(employee1);
            Console.WriteLine(employee2);
            Console.WriteLine(employee3);

            Console.WriteLine("\nСотрудники отдела:");

            foreach (EmployeeExpanded employee in department.Employees)
            {
                Console.WriteLine(employee.Name);
            }
        }

        /// <summary>
        /// Запрашивает у пользователя данные для создания расширенных сотрудников и отделов.
        /// </summary>
        private static void InputEmployeeExpandedTask()
        {
            int departmentCount;

            while (true)
            {
                Console.Write("\nВведите количество отделов: ");

                if (int.TryParse(Console.ReadLine(), out departmentCount)
                    && departmentCount > 0)
                {
                    break;
                }

                Console.WriteLine("Error: Enter positive integer.");
            }

            List<EmployeeExpanded> allEmployees = new List<EmployeeExpanded>();

            for (int d = 0; d < departmentCount; d++)
            {
                Console.WriteLine($"\nОтдел {d + 1}");

                DepartmentExpanded department = CreateDepartmentFromInput(allEmployees);

                Console.WriteLine($"\nСоздан отдел {department.Name}");
            }

            FindEmployeeDepartment(allEmployees);
        }

        /// <summary>
        /// Создает отдел на основе пользовательского ввода.
        /// </summary>
        /// <param name="allEmployees">Список всех сотрудников для добавления новых.</param>
        /// <returns>Созданный объект DepartmentExpanded.</returns>
        private static DepartmentExpanded CreateDepartmentFromInput(List<EmployeeExpanded> allEmployees)
        {
            string departmentName;

            while (true)
            {
                Console.Write("Введите название отдела: ");

                departmentName = Console.ReadLine() ?? string.Empty;

                if (!string.IsNullOrWhiteSpace(departmentName))
                {
                    break;
                }

                Console.WriteLine("Error: Empty name.");
            }

            DepartmentExpanded department = new DepartmentExpanded(departmentName);

            int employeeCount;

            while (true)
            {
                Console.Write("Введите количество сотрудников: ");

                if (int.TryParse(Console.ReadLine(), out employeeCount)
                    && employeeCount > 0)
                {
                    break;
                }

                Console.WriteLine("Error: Enter positive integer.");
            }

            List<EmployeeExpanded> employees = new List<EmployeeExpanded>();

            for (int i = 0; i < employeeCount; i++)
            {
                string employeeName;

                while (true)
                {
                    Console.Write($"Введите имя сотрудника {i + 1}: ");

                    employeeName = Console.ReadLine() ?? string.Empty;

                    if (!string.IsNullOrWhiteSpace(employeeName))
                    {
                        break;
                    }

                    Console.WriteLine("Error: Empty name.");
                }

                EmployeeExpanded employee = new EmployeeExpanded(employeeName, department);

                employees.Add(employee);
                allEmployees.Add(employee);
            }

            SelectManager(department, employees);

            return department;
        }

        /// <summary>
        /// Выбирает начальника отдела из списка сотрудников.
        /// </summary>
        /// <param name="department">Отдел, для которого выбирается начальник.</param>
        /// <param name="employees">Список сотрудников отдела.</param>
        private static void SelectManager(
            DepartmentExpanded department,
            List<EmployeeExpanded> employees)
        {
            int managerIndex;

            while (true)
            {
                Console.Write("Введите номер начальника: ");

                if (int.TryParse(Console.ReadLine(), out managerIndex)
                    && managerIndex >= 1
                    && managerIndex <= employees.Count)
                {
                    break;
                }

                Console.WriteLine("Error: Invalid number.");
            }

            department.Manager = employees[managerIndex - 1];
        }

        /// <summary>
        /// Находит отдел сотрудника по его имени и выводит информацию.
        /// </summary>
        /// <param name="allEmployees">Список всех сотрудников для поиска.</param>
        private static void FindEmployeeDepartment(List<EmployeeExpanded> allEmployees)
        {
            while (true)
            {
                Console.Write("\nВведите имя сотрудника: ");

                string searchName = Console.ReadLine() ?? string.Empty;

                EmployeeExpanded? employee =
                    allEmployees.Find(e => e.Name == searchName);

                if (employee == null)
                {
                    Console.WriteLine("Сотрудник не найден.");
                    continue;
                }

                Console.WriteLine($"\n{employee}");

                Console.WriteLine("Сотрудники отдела:");

                foreach (EmployeeExpanded emp in employee.Department.Employees)
                {
                    Console.WriteLine(emp.Name);
                }

                break;
            }
        }

        /// <summary>
        /// Выполняет задание №4.5 "Создаем имена".
        /// </summary>
        private static void RunConstrainedNameTask()
        {
            Console.WriteLine("№4.5 Имена с ограничениями");

            CreateExamplesConstrainedNameTask();
            InputConstrainedNameTask();
        }

        /// <summary>
        /// Создает примеры объектов NameConstrained для демонстрации.
        /// </summary>
        private static void CreateExamplesConstrainedNameTask()
        {
            NameConstrained name1 = new NameConstrained("Клеопатра");
            NameConstrained name2 = new NameConstrained("Александр", "Пушкин", "Сергеевич");
            NameConstrained name3 = new NameConstrained("Владимир", "Маяковский");

            Console.WriteLine("\nПримеры:");
            Console.WriteLine(name1);
            Console.WriteLine(name2);
            Console.WriteLine(name3);
        }

        /// <summary>
        /// Запрашивает у пользователя данные для создания объекта NameConstrained.
        /// </summary>
        private static void InputConstrainedNameTask()
        {
            Console.Write("\nВведите имя (обязательно): ");
            string firstNameCnst = Console.ReadLine() ?? string.Empty;

            Console.Write("Введите фамилию: ");
            string lastNameCnst = Console.ReadLine() ?? string.Empty;

            Console.Write("Введите отчество: ");
            string patronymicCnst = Console.ReadLine() ?? string.Empty;

            try
            {
                NameConstrained newNameCnst;

                if (!string.IsNullOrEmpty(firstNameCnst) && !string.IsNullOrEmpty(lastNameCnst) && !string.IsNullOrEmpty(patronymicCnst))
                {
                    newNameCnst = new NameConstrained(firstNameCnst, lastNameCnst, patronymicCnst);
                    Console.WriteLine($"Результат: {newNameCnst}");
                }
                else if (!string.IsNullOrEmpty(firstNameCnst) && !string.IsNullOrEmpty(lastNameCnst) && string.IsNullOrEmpty(patronymicCnst))
                {
                    newNameCnst = new NameConstrained(lastNameCnst, firstNameCnst);
                    Console.WriteLine($"Результат: {newNameCnst}");
                }
                else if (!string.IsNullOrEmpty(firstNameCnst) && string.IsNullOrEmpty(lastNameCnst) && string.IsNullOrEmpty(patronymicCnst))
                {
                    newNameCnst = new NameConstrained(firstNameCnst, lastNameCnst, patronymicCnst);
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
        /// </summary>
        private static void RunFractionTask()
        {
            Console.WriteLine("№5.5 Дроби");

            CreateExamplesFractionTask();
            InputFractionTask();
        }

        /// <summary>
        /// Создает примеры объектов Fraction и демонстрирует арифметические операции.
        /// </summary>
        private static void CreateExamplesFractionTask()
        {
            Fraction f1 = new Fraction(1, 2);
            Fraction f2 = new Fraction(2, 3);

            Console.WriteLine("\nПримеры:");

            Console.WriteLine($"{f1} + {f2} = {f1.Sum(f2)}");
            Console.WriteLine($"{f1} - {f2} = {f1.Minus(f2)}");
            Console.WriteLine($"{f1} * {f2} = {f1.Multiply(f2)}");
            Console.WriteLine($"{f1} / {f2} = {f1.Div(f2)}");
        }

        /// <summary>
        /// Запрашивает у пользователя данные для создания дробей и выполнения операций.
        /// </summary>
        private static void InputFractionTask()
        {
            try
            {
                Console.WriteLine("\nВведите первую дробь:");

                Fraction fraction1 = ReadFractionFromConsole("A");

                Console.WriteLine("\nВведите вторую дробь:");

                Fraction fraction2 = ReadFractionFromConsole("B");

                Console.WriteLine("\n(1) Сложение");
                Console.WriteLine("(2) Вычитание");
                Console.WriteLine("(3) Умножение");
                Console.WriteLine("(4) Деление");

                int operation;

                while (true)
                {
                    Console.Write("Введите номер операции: ");

                    if (int.TryParse(Console.ReadLine(), out operation)
                        && operation >= 1
                        && operation <= 4)
                    {
                        break;
                    }

                    Console.WriteLine("Error: Invalid number.");
                }

                switch (operation)
                {
                    case 1:
                        Console.WriteLine(fraction1.Sum(fraction2));
                        break;

                    case 2:
                        Console.WriteLine(fraction1.Minus(fraction2));
                        break;

                    case 3:
                        Console.WriteLine(fraction1.Multiply(fraction2));
                        break;

                    case 4:
                        Console.WriteLine(fraction1.Div(fraction2));
                        break;
                }
            }
            catch (Exception ex)
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