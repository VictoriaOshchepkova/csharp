using System;

namespace ConsoleApp2
{
    public class EmployeeExpanded
    {
        public string name;
        public DepartmentExpanded department;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DepartmentExpanded Department
        {
            get { return department; }
            set { department = value; }
        }

        public EmployeeExpanded(string name, DepartmentExpanded department)
        {
            Name = name;
            Department = department;
            department.AddEmployee(this);
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
