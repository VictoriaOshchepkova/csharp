namespace ConsoleApp2_1
{
    public class Employee
    {
        private string _name; // Приватные атрибуты, _camelCase 
        private Department _department;

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

        public Employee(string name, Department department)
        {
            Name = name;
            Department = department;
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
