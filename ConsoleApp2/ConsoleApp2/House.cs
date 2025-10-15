using System;

namespace ConsoleApp2
{
    public class House
    {
        private int floors;

        public int Floors
        {
            get { return floors; }
            set
            {
                if (value > 0)
                {
                    floors = value;
                }
                else
                {
                    throw new ArgumentException("Error: Invalid input");
                }
            }
        }

        public House(int floors)
        {
            Floors = floors;
        }

        public override string ToString()
        {
            string result;

            if (floors % 10 == 1 && floors % 100 != 11)
            {
                result = $"дом с {floors} этажом";
            }
            else
            {
                result = $"дом с {floors} этажами";
            }
            return result;

        }

    }
}
