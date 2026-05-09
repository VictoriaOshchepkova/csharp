using System.Collections.Generic;

namespace ConsoleApp2_1
{
    public class DepartmentExpanded
    {
        private string _name; // Приватные атрибуты, _camelCase 
        private EmployeeExpanded? _manager; // Nullable
        private readonly List<EmployeeExpanded> _employees;  // Вместо массива фикс.размера

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

        public EmployeeExpanded? Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }
        public IReadOnlyCollection<EmployeeExpanded> Employees // IReadOnlyCollection
        {
            get { return _employees.AsReadOnly(); }
        }

        public DepartmentExpanded(string name, EmployeeExpanded? manager = null)
        {
            Name = name;
            Manager = manager;
            _employees = new List<EmployeeExpanded>();
        }

        public void AddEmployee(EmployeeExpanded employee)
        {
            if (employee == null) // Добавление сотрудника без ограничений
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _employees.Add(employee);
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
