namespace ConsoleApp2_1
{
    public class EmployeeExpanded
    {
        private string _name; // Приватные атрибуты, _camelCase 
        private DepartmentExpanded _department;

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

        public EmployeeExpanded(string name, DepartmentExpanded department)
        {
            Name = name;
            Department = department;
            department.AddEmployee(this);
        }

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
