namespace ConsoleApp2_1
{
    public class NameConstrained : NameBase
    {
        private string? _firstName;  // Приватные атрибуты, _camelCase, nullable
        private string? _lastName;
        private string? _patronymic;

        public string? FirstName
        {
            get { return _firstName; }
            set
            {
                ValidateCapitalized(value, "firstName"); // DRY
                _firstName = value;
            }
        }

        public string? LastName
        {
            get { return _lastName; }
            set
            {
                ValidateCapitalized(value, "last name");
                _lastName = value;
            }
        }

        public string? Patronymic
        {
            get { return _patronymic; }
            set
            {
                ValidateCapitalized(value, "patronymic");
                _patronymic = value;
            }
        }

        public NameConstrained()
        {
            FirstName = "Иван"; // Использование сеттеров
            LastName = "Иванов";
            Patronymic = "Иванович";
        }

        public NameConstrained(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("Error: Name is required");
            }

            FirstName = firstName;
        }
        public NameConstrained(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Error: Name and last name are required");
            }

            FirstName = firstName;
            LastName = lastName;
        }
        public NameConstrained(string firstName, string lastName, string patronymic)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(patronymic))
            {
                throw new ArgumentException("Error: Full name is required");
            }

            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }

        public override string ToString()
        {
            var parts = new List<string?>(); // Лучше читаемость
            if (!string.IsNullOrEmpty(LastName))
            {
                parts.Add(LastName);
            }
            if (!string.IsNullOrEmpty(FirstName))
            {
                parts.Add(FirstName);
            }
            if (!string.IsNullOrEmpty(Patronymic))
            {
                parts.Add(Patronymic);
            }

            return string.Join(" ", parts);
        }
    }
}
