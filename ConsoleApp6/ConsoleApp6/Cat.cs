using System;

namespace ConsoleApp6
{
    /// <summary>
    /// Представляет кота с возможностью мяуканья.
    /// </summary>
    public class Cat: IMeowable
    {
        /// <summary>
        /// Имя кота.
        /// </summary>
        private string _name;

        /// <summary>
        /// Позволяет получить или установить имя кота.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если значение является пустым, null или состоит только из пробельных символов.
        /// </exception>
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Error: The cat's name cannot be empty.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Cat"/> с указанным именем.
        /// </summary>
        /// <param name="name">Имя кота.</param>
        public Cat(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Возвращает строковое представление кота в формате "кот: {Имя}".
        /// </summary>
        /// <returns>Строковое представление кота.</returns>
        public override string ToString()
        {
            return $"кот: {Name}";
        }

        /// <summary>
        /// Издает одиночное мяуканье, выводя сообщение в консоль.
        /// </summary>
        /// <remarks>
        /// Формат вывода: "{Имя}: мяу!"
        /// </remarks>
        public void Meow()
        {
            Console.WriteLine($"{Name}: мяу!");
        }

        /// <summary>
        /// Издает несколько мяуканий указанное количество раз.
        /// </summary>
        /// <param name="count">Количество мяуканий. Должно быть натуральным числом.</param>
        /// <exception cref="ArgumentException">
        /// Выбрасывается, если <paramref name="count"/> меньше или равен 0.
        /// </exception>
        /// <remarks>
        /// Формат вывода: "{Имя}: мяу-мяу-...-мяу!" (количество "мяу" равно <paramref name="count"/>).
        /// </remarks>
        public void Meow(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Error: The number of meows must be a natural number.");

            var meows = "";
            for (int i = 0; i < count; i++)
            {
                if (i > 0) meows += "-";
                meows += "мяу";
            }
            Console.WriteLine($"{Name}: {meows}!");
        }
    }
}
