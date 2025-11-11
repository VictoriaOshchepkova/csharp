using System;
using System.IO;
using System.Collections.Generic;

class Tasks1_5
{
    public static void InsertAfterFirstE(List<object> L, object E, List<object> L1)
    {
        if (L == null || L1 == null)
        {
            throw new Exception($"Error: One of the lists is null");
        }

        int index = L.IndexOf(E);

        if (index == -1)
        {
            throw new Exception($"Error: Element {E} not found");
        }

        L.InsertRange(index + 1, L1);
    }

    public static void InsertEBeginningAndEnd(LinkedList<object> L, object E)
    {
        if (L == null)
            throw new Exception($"Error: List {L} is empty");

        L.AddFirst(E);
        L.AddLast(E);
    }

    public static void AnalyzeBooks(HashSet<string> allBooks, HashSet<HashSet<string>> readersBooks)
    {
        if (allBooks == null || readersBooks == null)
        {
            throw new ArgumentException("Error: Invalid input data");
        }

        HashSet<string> readAll = new HashSet<string>(allBooks);
        foreach (HashSet<string> readerBooks in readersBooks)
        {
            readAll.IntersectWith(readerBooks);
        }

        HashSet<string> readSome = new HashSet<string>();
        foreach (HashSet<string> readerBooks in readersBooks)
        {
            readSome.UnionWith(readerBooks);
        }

        HashSet<string> readNone = new HashSet<string>(allBooks);
        readNone.ExceptWith(readSome);

        Console.WriteLine("\nКниги, прочитанные всеми читателями:");
        if (readAll.Count == 0)
            Console.WriteLine("(нет книг)");
        else
            foreach (string book in readAll)
                Console.WriteLine($"- {book}");

        Console.WriteLine("\nКниги, прочитанные некоторыми читателями:");
        if (readSome.Count == 0)
            Console.WriteLine("(нет книг)");
        else
            foreach (string book in readSome)
                Console.WriteLine($"- {book}");

        Console.WriteLine("\nКниги, которые никто не прочитал:");
        if (readNone.Count == 0)
            Console.WriteLine("(нет книг)");
        else
            foreach (string book in readNone)
                Console.WriteLine($"- {book}");
    }

    public static void PrintCharText(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            throw new ArgumentException("Error: The file doesn't exist or the path to it is empty");
        }

        string text = File.ReadAllText(filePath);

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Файл пуст или содержит только пробелы");
            return;
        }

        Console.WriteLine("Данные в файле:");
        Console.WriteLine(text);

        char[] separators = new char[] { ' ', ',', '.', '!', '?', ';', ':', '(', ')', '"', '\t', '\n', '\r' };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        if (words.Length < 2)
        {
            Console.WriteLine("\nВ тексте меньше 2 слов");
            return;
        }

        HashSet<char> firstWordChars = new HashSet<char>();
        foreach (char c in words[0])
        {
            firstWordChars.Add(c);
        }

        HashSet<HashSet<char>> otherWordsChar = new HashSet<HashSet<char>>();
        
        for (int i = 1; i < words.Length; i++)
        {
            HashSet<char> wordChars = new HashSet<char>();
            foreach (char c in words[i])
            {
                wordChars.Add(c);
            }
            otherWordsChar.Add(wordChars);
        }

        HashSet<char> commonChars = null;
        foreach (var charSet in otherWordsChar)
        {
            if (commonChars == null)
            {
                commonChars = new HashSet<char>(charSet);
            }
            else
            {
                commonChars.IntersectWith(charSet);
            }
        }

        commonChars.ExceptWith(firstWordChars);

        if (commonChars.Count == 0)
        {
            Console.WriteLine("\nСимволов, которых нет в первом слове, но есть во всех остальных, нет ");
        }
        else
        {
            Console.WriteLine("\nСимволы, которых нет в первом слове, но есть во всех остальных:");
            Console.WriteLine(string.Join(", ", commonChars));
        }
    }
    public static void FillDataFile(string filePath, int participantCount)
    {
        //Random random = new Random();
        //string[] surnames = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов", "Попов", "Васильев" };
        //string[] names = { "Алексей", "Дмитрий", "Сергей", "Андрей", "Михаил", "Владимир", "Олег" };

        //try
        //{
        //    using (StreamWriter writer = new StreamWriter(filePath))
        //    {
        //        writer.WriteLine(participantCount);

        //        Console.WriteLine("Данные в файле:");
        //        for (int i = 0; i < participantCount; i++)
        //        {
        //            string surname = surnames[random.Next(surnames.Length)];
        //            string name = names[random.Next(names.Length)];
        //            int grade = random.Next(7, 12);
        //            int score = random.Next(0, 71);

        //            writer.WriteLine($"{surname} {name} {grade} {score}");
        //            Console.WriteLine($"{surname} {name} {grade} {score}");
        //        }
        //    }
        //    Console.WriteLine($"Файл '{filePath}' успешно создан и заполнен");
        //}
        //catch
        //{
        //    Console.WriteLine($"Error: The file '{filePath}' cannot be created");
        //}

        string[] test1 = new string[]
        {
            "12",
            "Иванов Петр 7 68", //
            "Петров Иван 7 65",
            "Сидоров Алексей 7 62",
            "Козлова Мария 8 70", //
            "Никитин Сергей 8 67",
            "Федоров Дмитрий 8 64",
            "Семенова Анна 9 69", //
            "Васильев Павел 9 66",
            "Павлова Елена 9 63",
            "Алексеева Ольга 10 58",
            "Дмитриев Максим 11 55",
            "Павлова Марина 9 34",
        };

        string[] test2 = new string[]
        {
            "12",
            "Иванов Петр 7 68", //
            "Петров Иван 7 65",
            "Сидоров Алексей 7 62",
            "Козлова Мария 8 70", //
            "Никитин Сергей 8 67",
            "Федоров Дмитрий 8 64",
            "Семенова Анна 9 69", //
            "Васильев Павел 9 66",
            "Павлова Елена 9 68", //
            "Алексеева Ольга 10 68", //
            "Дмитриев Максим 11 55",
            "Павлова Марина 9 34",
        };

        string[] test3 = new string[]
        {
            "12",
            "Иванов Петр 7 34", 
            "Петров Иван 7 31",
            "Сидоров Алексей 7 15",
            "Козлова Мария 8 70", //
            "Никитин Сергей 8 6",
            "Федоров Дмитрий 8 12",
            "Семенова Анна 9 69", //
            "Васильев Павел 9 16",
            "Павлова Елена 9 34", 
            "Алексеева Ольга 10 34", 
            "Дмитриев Максим 11 5",
            "Павлова Марина 9 34",
        };

        string[] test4 = new string[]
{
            "12",
            "Иванов Петр 7 34",
            "Петров Иван 7 31",
            "Сидоров Алексей 7 15",
            "Козлова Мария 8 7", 
            "Никитин Сергей 8 6",
            "Федоров Дмитрий 8 12",
            "Семенова Анна 9 6", 
            "Васильев Павел 9 16",
            "Павлова Елена 9 34",
            "Алексеева Ольга 10 34",
            "Дмитриев Максим 11 5",
            "Павлова Марина 9 34",
};


        File.WriteAllLines(filePath, test1);
        Console.WriteLine($"Файл '{filePath}' успешно создан и заполнен");
        Console.WriteLine($"Данные в файле '{filePath}':");
        Console.WriteLine(string.Join("\n", test1));
    }

    public static void OlympiadTask(string inputFilePath)
    {
        string[] lines = File.ReadAllLines(inputFilePath);
        int n = int.Parse(lines[0]);

        SortedList<int, int> scoreQty = new SortedList<int, int>();
        Dictionary<int, SortedList<int, int>> gradeScoreQty = new Dictionary<int, SortedList<int, int>>();

        for (int i = 7; i <= 11; i++)
        {
            gradeScoreQty[i] = new SortedList<int, int>();
        }


        for (int i = 1; i <= n; i++)
        {
            string[] parts = lines[i].Split(' ');
            int grade = int.Parse(parts[2]);
            int score = int.Parse(parts[3]);

            if (scoreQty.ContainsKey(score))
            {
                scoreQty[score]++;
            }
            else
            {
                scoreQty[score] = 1;
            }

            if (gradeScoreQty[grade].ContainsKey(score))
            {
                gradeScoreQty[grade][score]++;
            }
            else
            {
                gradeScoreQty[grade][score] = 1;
            }

        }

        int topCount = (int)(n / 4);

        int currCount = 0;
        int minPrizeScore = 0;

        for (int i = scoreQty.Keys.Count - 1; i >= 0; i--)
        {
            int score = scoreQty.Keys[i];
            int count = scoreQty[score];

            if (currCount + count <= topCount && score > 35)
            {
                currCount += count;
                minPrizeScore = score;
            }
            else if (currCount + 1 <= topCount && score > 35)
            {
                minPrizeScore = score;
                break;
            }
        }

        if (minPrizeScore != 0)
        {
            Console.WriteLine($"\nМинимальный балл призера олимпиады: {minPrizeScore}");

            for (int classNum = 7; classNum <= 11; classNum++)
            {
                int count = 0;
                var classScores = gradeScoreQty[classNum];

                for (int i = classScores.Keys.Count - 1; i >= 0; i--)
                {
                    int score = classScores.Keys[i];
                    if (score >= minPrizeScore)
                    {
                        count += classScores[score];
                    }
                    else
                    {
                        break;
                    }
                }
                Console.WriteLine($"{classNum} класс: {count} призеров");
            }
        }
        else
        {
            Console.WriteLine($"\nНикто не стал призером олимпиады");
        }
    }
}



