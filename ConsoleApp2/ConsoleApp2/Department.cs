using System;

namespace ConsoleApp2
{
    public class Department
    {
        string name;
        Employee manager;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Employee Manager
        {
            get { return manager; }
            set { manager = value; }
        }

        public Department(string name, Employee manager = null)
        {
            Name = name;
            Manager = manager;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
