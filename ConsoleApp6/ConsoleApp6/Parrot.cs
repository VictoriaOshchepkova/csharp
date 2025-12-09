using ConsoleApp6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp6
{
    /// <summary>
    /// Представляет попугая с возможностью мяуканья.
    /// </summary>
    public class Parrot : IMeowable
    {
        /// <summary>
        /// Имя попугая.
        /// </summary>
        private string _name;

        /// <summary>
        /// Позволяет получить или установить имя попугая.
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
                    throw new ArgumentException("Error: The parrot's name cannot be empty.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Parrot"/> с указанным именем.
        /// </summary>
        /// <param name="name">Имя попугая.</param>
        public Parrot(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Возвращает строковое представление попугая в формате "Попугай: {Имя}".
        /// </summary>
        /// <returns>Строковое представление попугая.</returns>
        public override string ToString()
        {
            return $"Робокот: {Name}";
        }

        /// <summary>
        /// Издает одиночное мяуканье, выводя сообщение в консоль.
        /// </summary>
        /// <remarks>
        /// Формат вывода: "{Имя}: чирик-мяу!"
        /// </remarks>
        public void Meow()
        {
            Console.WriteLine($"{Name}: чирик-мяу!");
        }
    }
}