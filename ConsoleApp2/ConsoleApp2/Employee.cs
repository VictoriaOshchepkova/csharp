using System;

namespace ConsoleApp2
{
    public class Employee
    {
        string name;
        Department department;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Department Department
        {
            get { return department; }
            set { department = value; }
        }

        public Employee(string name, Department department)
        {
            Name = name;
            Department = department;
        }

        public override string ToString()
        {
            if (department.Manager == this)
            {
                return $"{name} начальник отдела {department.Name}";
            }
            else if (department.Manager != null)
            {
                return $"{name} работает в отделе {department.Name}, начальник которого {department.Manager.Name}";
            }
            else
            {
                return $"{name} работает в отделе {department.Name} (начальник не назначен)";
            }
        }
    }
}
