namespace ConsoleApp2_1
{
    /// <summary>
    /// Представляет отдел компании с названием и руководителем.
    /// </summary>
    public class Department
    {
        private string _name; // Приватные атрибуты, _camelCase 
        private Employee? _manager; // Nullable

        /// <summary>
        /// Получает или задает название отдела.
        /// </summary>
        /// <exception cref="ArgumentNullException">Выбрасывается, когда значение равно null.</exception>
        public string Name
        {
            get { return _name; }
            set // Guard clause
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _name = value;
            }
        }

        /// <summary>
        /// Получает или задает руководителя отдела.
        /// </summary>
        public Employee? Manager
        {
            get { return _manager; }
            set { _manager = value; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса Department.
        /// </summary>
        /// <param name="name">Название отдела.</param>
        /// <param name="manager">Руководитель отдела (необязательно).</param>
        public Department(string name, Employee manager = null)
        {
            Name = name;
            Manager = manager;
        }

        /// <summary>
        /// Возвращает строковое представление отдела.
        /// </summary>
        /// <returns>Название отдела.</returns>
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}