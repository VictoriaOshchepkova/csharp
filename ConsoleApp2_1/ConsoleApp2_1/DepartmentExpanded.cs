namespace ConsoleApp2_1
{
    /// <summary>
    /// Представляет расширенную версию отдела компании со списком сотрудников.
    /// </summary>
    public class DepartmentExpanded
    {
        private string _name; // Приватные атрибуты, _camelCase 
        private EmployeeExpanded? _manager; // Nullable
        private readonly List<EmployeeExpanded> _employees;  // Вместо массива фикс.размера

        /// <summary>
        /// Получает или задает название отдела.
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
        /// Получает или задает руководителя отдела.
        /// </summary>
        public EmployeeExpanded? Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        /// <summary>
        /// Получает список сотрудников отдела (только для чтения).
        /// </summary>
        public IReadOnlyCollection<EmployeeExpanded> Employees // IReadOnlyCollection
        {
            get { return _employees.AsReadOnly(); }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса DepartmentExpanded.
        /// </summary>
        /// <param name="name">Название отдела.</param>
        /// <param name="manager">Руководитель отдела (необязательно).</param>
        public DepartmentExpanded(string name, EmployeeExpanded? manager = null)
        {
            Name = name;
            Manager = manager;
            _employees = new List<EmployeeExpanded>();
        }

        /// <summary>
        /// Добавляет сотрудника в отдел.
        /// </summary>
        /// <param name="employee">Добавляемый сотрудник.</param>
        /// <exception cref="ArgumentNullException">Выбрасывается, когда сотрудник равен null.</exception>
        public void AddEmployee(EmployeeExpanded employee)
        {
            if (employee == null) // Добавление сотрудника без ограничений
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _employees.Add(employee);
        }

        /// <summary>
        /// Возвращает строковое представление отдела.
        /// </summary>
        /// <returns>Название отдела.</returns>
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}