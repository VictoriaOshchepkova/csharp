using System;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApp3
{
    [Serializable]
    public struct Toy
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }

        public Toy(string name, decimal price, int minAge, int maxAge)
        {
            Name = name;
            Price = price;
            MinAge = minAge;
            MaxAge = maxAge;
        }
    }
    class Files
    {
        public static void WriteRandomNumbers(int count)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open("task4.bin", FileMode.Create)))
                {
                    Random random = new Random();
                    for (int i = 0; i < count; ++i)
                    {
                        writer.Write(random.Next(1, 101));
                    }
                }
                Console.WriteLine($"Файл 'task4.bin' успешно создан и заполнен {count} числами");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Error: The file cannot be created");
            }
        }
        public static void ReadNumbers()
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open("task4fin.bin", FileMode.Create)))
                {
                    int length = 0;
                    int number;

                    using (BinaryReader reader = new BinaryReader(File.Open("task4.bin", FileMode.Open)))
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            number = reader.ReadInt32();
                            ++length;
                        }
                    }

                    int[] arr = new int[length];
                    int currPos = 0;

                    using (BinaryReader reader = new BinaryReader(File.Open("task4.bin", FileMode.Open)))
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            arr[currPos] = reader.ReadInt32();
                            ++currPos;
                        }
                    }

                    for (int i = 0; i < length; ++i)
                    {
                        for (int j = 0; j <= i; ++j)
                        {
                            writer.Write(arr[j]);
                        }
                    }

                }

                Console.WriteLine("Числа записаны в файл 'task4fin.bin'");
            }
            catch
            {
                Console.WriteLine(" Error: The file cannot be opened");
            }
        }

        public static void CreateToysFile()
        {
            Toy[] toys = new Toy[8]
            {
                new Toy("Конструктор Lego", 2500, 3, 12),
                new Toy("Кукла Barbie", 1500, 3, 8),
                new Toy("Набор химика", 3200, 10, 16),
                new Toy("Пазл 100 деталей", 800, 4, 10),
                new Toy("Машинка на радиоуправлении", 4500, 5, 15),
                new Toy("Мягкий мишка", 600, 1, 6),
                new Toy("Набор доктора", 1200, 4, 12),
                new Toy("Энциклопедия", 2000, 8, 16)
            };

            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            using (FileStream fs = new FileStream("task5.bin", FileMode.Create))
            {
                serializer.Serialize(fs, toys);
            }

            Console.WriteLine($"Файл 'task5.bin' успешно создан.");
        }

        public static void GetToysForAges()
        {
            Toy[] toys;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
                using (FileStream fs = new FileStream("task5.bin", FileMode.Open))
                {
                    toys = (Toy[])serializer.Deserialize(fs);
                }

                int count = 0;
                for (int i = 0; i < toys.Length; i++)
                {
                    if (toys[i].MinAge <= 4 && toys[i].MaxAge >= 4 &&
                        toys[i].MinAge <= 10 && toys[i].MaxAge >= 10)
                    {
                        count++;
                    }
                }

                string[] suitableToys = new string[count];
                int index = 0;

                for (int i = 0; i < toys.Length; i++)
                {
                    if (toys[i].MinAge <= 4 && toys[i].MaxAge >= 10)
                    {
                        suitableToys[index] = toys[i].Name;
                        index++;
                    }
                }

                Console.WriteLine("Игрушки, подходящие детям как 4, так и 10 лет:");
                if (suitableToys.Length == 0)
                {
                    Console.WriteLine("Таких игрушек не найдено");
                }
                else
                {
                    for (int i = 0; i < suitableToys.Length; i++)
                    {
                        Console.WriteLine($"- {suitableToys[i]}");
                    }
                }

            }
            catch
            {
                Console.WriteLine(" Error: The file cannot be opened");
            }
        }

        public static void WriteRandomNumbersNewString(int count)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(File.Open("task6.txt", FileMode.Create)))
                {
                    Random random = new Random();
                    for (int i = 0; i < count; ++i)
                    {
                        writer.WriteLine(random.Next(-100, 101));
                    }
                }
                Console.WriteLine($"Файл 'task6.txt' успешно создан и заполнен {count} числами");
            }
            catch
            {
                Console.WriteLine(" Error: The file cannot be created");
            }
        }

        public static void FindMinMax()
        {
            bool isFirst = true;
            int minNum = 0;
            int maxNum = 0;
            int currNum;

            try
            {
                using (StreamReader reader = new StreamReader("task6.txt"))
                {
                    Console.Write("Данные в файле: ");
                    while (!reader.EndOfStream)
                    {
                        currNum = int.Parse(reader.ReadLine());
                        Console.Write($"{currNum} ");

                        if (isFirst == true)
                        {
                            minNum = currNum;
                            maxNum = currNum;
                            isFirst = false;
                        }
                        else if (minNum > currNum)
                        {
                            minNum = currNum;
                        }
                        else if (maxNum < currNum)
                        {
                            maxNum = currNum;
                        }
                    }
                }

                int product = minNum * maxNum;

                Console.WriteLine($"\nМинимальный элемент: {minNum}");
                Console.WriteLine($"Максимальный элемент: {maxNum}");
                Console.WriteLine($"Произведение минимального и максимального элементов: {product}");
            }
            catch
            {
                Console.WriteLine(" Error: The file cannot be opened");
            }
        }

        public static void WriteRandomNumbersTextFile(int count)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(File.Open("task7.txt", FileMode.Create)))
                {
                    Random random = new Random();
                    for (int i = 0; i < count; ++i)
                    {
                        writer.Write(random.Next(-100, 101));
                        if (i < count - 1)
                        {
                            writer.Write(" ");
                        }
                    }
                }
                Console.WriteLine($"Файл 'task7.txt' успешно создан и заполнен {count} числами");
            }
            catch
            {
                Console.WriteLine(" Error: The file cannot be created");
            }
        }

        public static void CountOddNumbers()
        {
            string[] strArray;
            int currNum;
            int count = 0;

            try
            {
                using (StreamReader reader = new StreamReader("task7.txt"))
                {
                    strArray = reader.ReadLine().Split(' ');
                }

                for (int i = 0; i < strArray.Length; ++i)
                {
                    try
                    {
                        currNum = int.Parse(strArray[i]);
                        if (currNum % 2 != 0)
                        {
                            ++count;
                        }
                    }
                    catch
                    {
                        Console.WriteLine($"Элемент '{strArray[i]}' не является целым числом");
                    }
                }

                Console.WriteLine($"Данные в файле: {string.Join(" ", strArray)}");
                Console.WriteLine($"Количество нечетных чисел: {count}");
            }
            catch
            {
                Console.WriteLine(" Error: The file cannot be opened");
            }
        }

        public static void CreateNewFileLengthString()
        {
            string s;

            try
            {
                using (StreamReader reader = new StreamReader("task8.txt"))
                using (StreamWriter writer = new StreamWriter(File.Open("task8fin.txt", FileMode.Create)))
                {
                    Console.WriteLine("Данные в исходном файле:");
                    while ((s = reader.ReadLine()) != null)
                    {
                        Console.WriteLine($"{s}\t| Длина строки: {s.Length}");
                        writer.WriteLine(s.Length);
                    }
                }

                Console.WriteLine($"Файл 'task8fin.txt' успешно создан!");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(" Error: The file cannot be created");
            }
        }
    }
}
