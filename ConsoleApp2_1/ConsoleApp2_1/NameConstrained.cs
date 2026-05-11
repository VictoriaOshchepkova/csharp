namespace ConsoleApp2_1
{
    /// <summary>
    /// Представляет имя с ограничениями: обязательно должно быть указано хотя бы имя.
    /// </summary>
    public class NameConstrained : NameBase
    {
        private string? _firstName;  // Приватные атрибуты, _camelCase, nullable
        private string? _lastName;
        private string? _patronymic;

        /// <summary>
        /// Получает или задает имя.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если имя начинается не с заглавной буквы.</exception>
        public string? FirstName
        {
            get { return _firstName; }
            set
            {
                ValidateCapitalized(value, "firstName"); // DRY
                _firstName = value;
            }
        }

        /// <summary>
        /// Получает или задает фамилию.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если фамилия начинается не с заглавной буквы.</exception>
        public string? LastName
        {
            get { return _lastName; }
            set
            {
                ValidateCapitalized(value, "last name");
                _lastName = value;
            }
        }

        /// <summary>
        /// Получает или задает отчество.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если отчество начинается не с заглавной буквы.</exception>
        public string? Patronymic
        {
            get { return _patronymic; }
            set
            {
                ValidateCapitalized(value, "patronymic");
                _patronymic = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса NameConstrained со значениями по умолчанию "Иван Иванов Иванович".
        /// </summary>
        public NameConstrained()
        {
            FirstName = "Иван"; // Использование сеттеров
            LastName = "Иванов";
            Patronymic = "Иванович";
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса NameConstrained только с именем.
        /// </summary>
        /// <param name="firstName">Имя (обязательно).</param>
        /// <exception cref="ArgumentException">Выбрасывается, если имя не указано.</exception>
        public NameConstrained(string firstName)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentException("Error: Name is required");
            }

            FirstName = firstName;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса NameConstrained с именем и фамилией.
        /// </summary>
        /// <param name="firstName">Имя (обязательно).</param>
        /// <param name="lastName">Фамилия (обязательна).</param>
        /// <exception cref="ArgumentException">Выбрасывается, если имя или фамилия не указаны.</exception>
        public NameConstrained(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Error: Name and last name are required");
            }

            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса NameConstrained с полным именем.
        /// </summary>
        /// <param name="firstName">Имя (обязательно).</param>
        /// <param name="lastName">Фамилия (обязательна).</param>
        /// <param name="patronymic">Отчество (обязательно).</param>
        /// <exception cref="ArgumentException">Выбрасывается, если какое-либо поле не указано.</exception>
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

        /// <summary>
        /// Возвращает строковое представление имени, объединяя все непустые компоненты через пробел.
        /// </summary>
        /// <returns>Строка вида "Фамилия Имя Отчество" (пропуская отсутствующие компоненты).</returns>
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