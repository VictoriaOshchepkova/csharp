namespace ConsoleApp2_1
{
    /// <summary>
    /// Представляет полное имя человека, состоящее из фамилии, имени и отчества.
    /// Поля могут быть не указаны (null).
    /// </summary>
    public class Name : NameBase
    {
        private string? _lastName;  // Приватные атрибуты, _camelCase 
        private string? _firstName;
        private string? _patronymic;

        /// <summary>
        /// Получает или задает фамилию.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если фамилия начинается не с заглавной буквы.</exception>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                ValidateCapitalized(value, "last name");  // DRY
                _lastName = value;
            }
        }

        /// <summary>
        /// Получает или задает имя.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если имя начинается не с заглавной буквы.</exception>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                ValidateCapitalized(value, "name");
                _firstName = value;
            }
        }

        /// <summary>
        /// Получает или задает отчество.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, если отчество начинается не с заглавной буквы.</exception>
        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                ValidateCapitalized(value, "patronymic");
                _patronymic = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса Name со значениями по умолчанию "Иванов Иван Иванович".
        /// </summary>
        public Name()
        {
            LastName = "Иванов"; // Использование сеттеров
            FirstName = "Иван";
            Patronymic = "Иванович";
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса Name с указанными компонентами.
        /// </summary>
        /// <param name="lastName">Фамилия (может быть null).</param>
        /// <param name="firstName">Имя (может быть null).</param>
        /// <param name="patronymic">Отчество (может быть null).</param>
        /// <exception cref="ArgumentException">Выбрасывается, если какой-либо компонент начинается не с заглавной буквы.</exception>
        public Name(string? lastName, string? firstName, string? patronymic)
        {
            LastName = lastName;
            FirstName = firstName;
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