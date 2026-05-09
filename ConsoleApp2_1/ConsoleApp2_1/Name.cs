namespace ConsoleApp2_1
{
    public class Name : NameBase
    {
        private string? _lastName;  // Приватные атрибуты, _camelCase 
        private string? _firstName;
        private string? _patronymic;

        public string LastName
        {
            get { return _lastName; }
            set
            {
                ValidateCapitalized(value, "last name");  // DRY
                _lastName = value;
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                ValidateCapitalized(value, "name");
                _firstName = value;
            }
        }

        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                ValidateCapitalized(value, "patronymic");
                _patronymic = value;
            }
        }

        public Name()
        {
            LastName = "Иванов"; // Использование сеттеров
            FirstName = "Иван";
            Patronymic = "Иванович";
        }

        public Name(string? lastName, string? firstName, string? patronymic)
        {
            LastName = lastName;
            FirstName = firstName;
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
