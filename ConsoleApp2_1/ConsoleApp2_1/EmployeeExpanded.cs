namespace ConsoleApp2_1
{
    /// <summary>
    /// Представляет расширенную версию сотрудника, который автоматически добавляется в отдел при создании.
    /// </summary>
    public class EmployeeExpanded
    {
        private string _name; // Приватные атрибуты, _camelCase 
        private DepartmentExpanded _department;

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
        public DepartmentExpanded Department
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
        /// Инициализирует новый экземпляр класса EmployeeExpanded.
        /// Автоматически добавляет сотрудника в указанный отдел.
        /// </summary>
        /// <param name="name">Имя сотрудника.</param>
        /// <param name="department">Отдел, в котором работает сотрудник.</param>
        public EmployeeExpanded(string name, DepartmentExpanded department)
        {
            Name = name;
            Department = department;
            department.AddEmployee(this);
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