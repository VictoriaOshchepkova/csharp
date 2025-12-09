using System;

namespace ConsoleApp6
{
    /// <summary>
    /// Декоратор, который подсчитывает количество мяуканий объекта, способного это делать.
    /// </summary>
    public class MeowCounter: IMeowable
    {
        /// <summary>
        /// Объект, способный мяукать.
        /// </summary>
        private readonly IMeowable _meowable;

        /// <summary>
        /// Счетчик мяуканий.
        /// </summary>
        private int _meowCount;

        /// <summary>
        /// Позволяет получить объект, для которого подсчитываются мяуканья.
        /// </summary>
        public IMeowable Meowable
        {
            get { return _meowable; }
        }

        /// <summary>
        /// Позволяет получить текущее количество мяуканий объекта.
        /// </summary>
        public int MeowCount
        {
            get { return _meowCount; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MeowCounter"/> для указанного объекта.
        /// </summary>
        /// <param name="meowable">Объект, для которого будет вестись подсчет мяуканий.</param>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если <paramref name="meowable"/> равен null/>.
        /// </exception>
        public MeowCounter(IMeowable meowable)
        {
            if (meowable == null)
            {
                throw new ArgumentNullException(nameof(meowable));
            }
            _meowable = meowable;
            _meowCount = 0;
        }

        /// <summary>
        /// Вызывает мяуканье у декорируемого объекта и увеличивает счётчик мяуканий.
        /// </summary>
        public void Meow()
        {
            _meowable.Meow();
            _meowCount++;
        }
    }
}
