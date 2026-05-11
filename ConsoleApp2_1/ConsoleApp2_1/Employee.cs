namespace ConsoleApp2_1
{
    /// <summary>
    /// Представляет сотрудника компании с именем и отделом.
    /// </summary>
    public class Employee
    {
        private string _name; // Приватные атрибуты, _camelCase 
        private Department _department;

        /// <summary>
        /// Получает или задает имя сотрудника.
        /// </summary>
        /// <exception cref="ArgumentNullException">Выбрасывается, когда значение равно null.</exception>
        public string Name
        {
            get { return _name; }
            set // Guard clause
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _name = value;
            }
        }

        /// <summary>
        /// Получает или задает отдел, в котором работает сотрудник.
        /// </summary>
        /// <exception cref="ArgumentNullException">Выбрасывается, когда значение равно null.</exception>
        public Department Department
        {
            get { return _department; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _department = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса Employee.
        /// </summary>
        /// <param name="name">Имя сотрудника.</param>
        /// <param name="department">Отдел, в котором работает сотрудник.</param>
        public Employee(string name, Department department)
        {
            Name = name;
            Department = department;
        }

        /// <summary>
        /// Возвращает строковое представление сотрудника.
        /// </summary>
        /// <returns>
        /// Если сотрудник является руководителем отдела: "Имя начальник отдела Название".
        /// Если руководитель назначен: "Имя работает в отделе Название, начальник которого Имя".
        /// Если руководитель не назначен: "Имя работает в отделе Название (начальник не назначен)".
        /// </returns>
        public override string ToString()
        {
            if (Department.Manager == this) // Использование геттеров
            {
                return $"{Name} начальник отдела {Department.Name}";
            }
            if (Department.Manager != null) // Убрала лишний else
            {
                return $"{Name} работает в отделе {Department.Name}, начальник которого {Department.Manager.Name}";
            }
            return $"{Name} работает в отделе {Department.Name} (начальник не назначен)";
        }
    }
}