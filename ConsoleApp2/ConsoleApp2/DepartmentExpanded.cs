using System;

namespace ConsoleApp2
{
    // Задание 3.4
    public class DepartmentExpanded
    {
        string name;
        EmployeeExpanded manager;
        EmployeeExpanded[] employees;
        int employeeCount;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public EmployeeExpanded Manager
        {
            get { return manager; }
            set { manager = value; }
        }
        public EmployeeExpanded[] Employees
        {
            get { return employees; }
            private set { employees = value; }
        }
        public int EmployeeCount
        {
            get { return employeeCount; }
            private set { employeeCount = value; }
        }

        public DepartmentExpanded(string name, EmployeeExpanded manager = null)
        {
            Name = name;
            Manager = manager;
            Employees = new EmployeeExpanded[100];
        }

        public void AddEmployee(EmployeeExpanded employee)
        {
            if (employeeCount < 100)
            {
                employees[employeeCount] = employee;
                employeeCount++;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
