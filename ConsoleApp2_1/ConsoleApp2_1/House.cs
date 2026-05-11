namespace ConsoleApp2_1
{
    /// <summary>
    /// Представляет дом с определенным количеством этажей.
    /// </summary>
    public class House
    {
        private int _floors; // _camelCase 

        /// <summary>
        /// Получает или задает количество этажей в доме.
        /// </summary>
        /// <exception cref="ArgumentException">Выбрасывается, когда значение меньше или равно 0.</exception>
        public int Floors
        {
            get { return _floors; }
            set
            {
                if (value <= 0) // Убрала лишний else
                {
                    throw new ArgumentException("Error: the number of floors must be positive.");
                }
                _floors = value;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса House.
        /// </summary>
        /// <param name="floors">Количество этажей (должно быть положительным).</param>
        /// <exception cref="ArgumentException">Выбрасывается, когда количество этажей меньше или равно 0.</exception>
        public House(int floors)
        {
            Floors = floors;
        }

        /// <summary>
        /// Возвращает строковое представление дома с правильным падежом для слова "этаж".
        /// </summary>
        /// <returns>Строка вида "дом с N этажом" или "дом с N этажами".</returns>
        public override string ToString()
        {
            string result;

            if (Floors % 10 == 1 && Floors % 100 != 11) // Использование геттеров
            {
                result = $"дом с {Floors} этажом";
            }
            else
            {
                result = $"дом с {Floors} этажами";
            }

            return result;
        }
    }
}