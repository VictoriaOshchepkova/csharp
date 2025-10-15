using System;

namespace ConsoleApp2
{
    public class NameСonstrained
    {
        string firstName;
        string lastName;
        string patronymic;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !char.IsUpper(value[0]))
                {
                    throw new ArgumentException("Error: The name must begin with a capital letter");
                }
                firstName = value;
            }
        }

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !char.IsUpper(value[0]))
                {
                    throw new ArgumentException("Error: Last name must begin with a capital letter");
                }
                lastName = value;
            }
        }

        public string Patronymic
        {
            get { return patronymic; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !char.IsUpper(value[0]))
                {
                    throw new ArgumentException("Error: The patronymic must begin with a capital letter");
                }
                patronymic = value;
            }
        }

        public NameСonstrained()
        {
            firstName = "Иван";
            lastName = "Иванов";
            patronymic = "Иванович";
        }

        public NameСonstrained(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("Error: Invalid input");
            }
            FirstName = firstName;
        }
        public NameСonstrained(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Error: Invalid input");
            }
            FirstName = firstName;
            LastName = lastName;
        }
        public NameСonstrained(string firstName, string lastName, string patronymic)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(patronymic))
            {
                throw new ArgumentException("Error: Invalid input");
            }
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }

        public override string ToString()
        {
            string result = "";

            if (!string.IsNullOrEmpty(firstName))
                result += firstName;

            if (!string.IsNullOrEmpty(lastName))
            {
                if (result != "")
                {
                    result += " ";
                }
                result += lastName;
            }

            if (!string.IsNullOrEmpty(patronymic))
            {
                if (result != "")
                {
                    result += " ";
                }
                result += patronymic;
            }
            return result;
        }
    }
}
