using System;

namespace ConsoleApp2
{
    public class Name
    {
        string lastName;
        string firstName;
        string patronymic;

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

        public Name()
        {
            lastName = "Иванов";
            firstName = "Иван";
            patronymic = "Иванович";
        }

        public Name(string lastName = null, string firstName = null, string patronymic = null)
        {
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
        }

        public override string ToString()
        {
            string result = "";

            if (!string.IsNullOrEmpty(lastName))
                result += lastName;

            if (!string.IsNullOrEmpty(firstName))
            {
                if (result != "")
                {
                    result += " ";
                }
                result += firstName;
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
