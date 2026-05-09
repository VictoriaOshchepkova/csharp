namespace ConsoleApp2_1
{
    public class Department
    {
        private string _name; // Приватные атрибуты, _camelCase 
        private Employee? _manager; // Nullable

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

        public Employee? Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        public Department(string name, Employee manager = null)
        {
            Name = name;
            Manager = manager;
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
