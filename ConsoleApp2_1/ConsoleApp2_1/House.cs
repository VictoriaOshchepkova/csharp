namespace ConsoleApp2_1
{
    public class House
    {
        private int _floors; // _camelCase 

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

        public House(int floors)
        {
            Floors = floors;
        }

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
