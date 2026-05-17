using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Главный класс программы.
/// </summary>
internal static class Program
{
    private static PizzaRepository _repository = new PizzaRepository(); // _camelCase

    /// <summary>
    /// Точка входа в программу.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.Write(
@"=== ВЫБОР ИСТОЧНИКА ДАННЫХ ===
1. Загрузить данные из файла LR5-var8.xls
2. Создать тестовую базу данных
0. Выход
Выберите пункт меню: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        _repository.LoadData();
                        ShowMainMenu();
                        break;

                    case "2":
                        _repository.CreateTestDB();
                        ShowMainMenu();
                        break;

                    case "0":
                        Console.WriteLine("Выход из программы...");
                        return;

                    default:
                        Console.WriteLine("Такого пункта нет. Выберите число от 0 до 2.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Отображает главное меню.
    /// </summary>
    private static void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.Write(
@"=== СИСТЕМА УПРАВЛЕНИЯ ПИЦЦЕРИИ ===
1. Просмотр базы данных
2. Удаление объектов
3. Добавление объектов
4. Получение отчетов
5. Сохранение изменений
0. Выход в главное меню
Выберите пункт меню: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        ViewDatabase();
                        break;

                    case "2":
                        DeleteItems();
                        break;

                    case "3":
                        AddItems();
                        break;

                    case "4":
                        ExecuteQueries();
                        break;

                    case "5":
                        _repository.SaveToExcel();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Такого пункта нет. Выберите число от 0 до 5.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Отображает базу данных для просмотра.
    /// </summary>
    private static void ViewDatabase()
    {
        Console.Clear();
        Console.Write(
@"=== ПРОСМОТР БАЗЫ ДАННЫХ ===
1. Клиенты
2. Заказы
3. Состав заказов
4. Меню
Выберите таблицу: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("\n=== КЛИЕНТЫ ===");

                foreach (var client in _repository.Clients)
                {
                    Console.WriteLine(client);
                }

                break;

            case "2":
                Console.WriteLine("\n=== ЗАКАЗЫ ===");

                foreach (var order in _repository.Orders)
                {
                    Console.WriteLine(order);
                }

                break;

            case "3":
                Console.WriteLine("\n=== СОСТАВ ЗАКАЗОВ ===");

                foreach (var item in _repository.OrderItems)
                {
                    Console.WriteLine(item);
                }

                break;

            case "4":
                Console.WriteLine("\n=== МЕНЮ ===");

                foreach (var item in _repository.Menu)
                {
                    Console.WriteLine(item);
                }

                break;

            default:
                Console.WriteLine("Такого пункта нет. Выберите число от 1 до 4.");
                break;
        }
    }

    /// <summary>
    /// Удаляет объекты из базы данных.
    /// </summary>
    private static void DeleteItems()
    {
        Console.Clear();
        Console.Write(
@"=== УДАЛЕНИЕ ОБЪЕКТОВ ===
1. Удалить клиента
2. Удалить заказ
3. Удалить блюдо из меню
4. Удалить позицию из заказа
Выберите действие: ");

        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    DeleteEntity(
                        "код клиента",
                        id => _repository.RemoveClient(id),
                        "Клиент успешно удален.",
                        "Клиент с указанным кодом не найден.");
                    break;

                case "2":
                    DeleteEntity(
                        "код заказа",
                        id => _repository.RemoveOrder(id),
                        "Заказ успешно удален.",
                        "Заказ с указанным кодом не найден.");
                    break;

                case "3":
                    DeleteEntity(
                        "код блюда",
                        id => _repository.RemoveMenuItem(id),
                        "Блюдо успешно удалено из меню.",
                        "Блюдо с указанным кодом не найдено.");
                    break;

                case "4":
                    DeleteEntity(
                        "код позиции заказа",
                        id => _repository.RemoveOrderItem(id),
                        "Позиция заказа успешно удалена.",
                        "Позиция заказа с указанным кодом не найдена.");
                    break;

                default:
                    Console.WriteLine("Такого пункта нет. Введите число от 1 до 4.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении данных: {ex.Message}");
        }
    }

    /// <summary>
    /// Универсальный метод для удаления сущности по идентификатору.
    /// </summary>
    /// <param name="entityName">Название идентификатора для вывода.</param>
    /// <param name="removeAction">Действие удаления.</param>
    /// <param name="successMessage">Сообщение при успешном удалении.</param>
    /// <param name="notFoundMessage">Сообщение при отсутствии сущности.</param>
    private static void DeleteEntity(
        string entityName,
        Func<int, bool> removeAction,
        string successMessage,
        string notFoundMessage)
    {
        Console.Write($"Введите {entityName}: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (removeAction(id))
            {
                Console.WriteLine(successMessage);
            }
            else
            {
                Console.WriteLine(notFoundMessage);
            }
        }
        else
        {
            Console.WriteLine("Значение должно являться натуральным числом.");
        }
    }

    /// <summary>
    /// Добавляет объекты в базу данных.
    /// </summary>
    private static void AddItems()
    {
        Console.Clear();
        Console.Write(
@"=== ДОБАВЛЕНИЕ ОБЪЕКТОВ ===
1. Добавить клиента
2. Добавить заказ
3. Добавить блюдо в меню
4. Добавить позицию в заказ
Выберите действие: ");

        var choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    AddClient();
                    break;

                case "2":
                    AddOrder();
                    break;

                case "3":
                    AddMenuItem();
                    break;

                case "4":
                    AddOrderItem();
                    break;

                default:
                    Console.WriteLine("Такого пункта нет. Введите число от 1 до 4.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении данных: {ex.Message}");
        }
    }

        /// <summary>
    /// Добавляет нового клиента.
    /// </summary>
    private static void AddClient()
    {
        Console.Write("Фамилия: ");
        var lastName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine("Фамилия не может быть пустой.");
            return;
        }

        Console.Write("Имя: ");
        var name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Имя не может быть пустым.");
            return;
        }

        Console.Write("Отчество: ");
        var patronymic = Console.ReadLine();

        Console.Write("Город: ");
        var city = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine("Город не может быть пустым.");
            return;
        }

        _repository.AddClient(new Client(0, lastName, name, patronymic, $"г. {city}"));
        Console.WriteLine("Клиент успешно добавлен.");
    }

    /// <summary>
    /// Добавляет новый заказ.
    /// </summary>
    private static void AddOrder()
    {
        Console.Write("Дата (дд.мм.гггг): ");
        var dateInput = Console.ReadLine();

        if (!DateTime.TryParse(dateInput, out DateTime date))
        {
            Console.WriteLine("Неверный формат даты. Используйте дд.мм.гггг.");
            return;
        }

        Console.Write("Код клиента: ");

        if (!int.TryParse(Console.ReadLine(), out int clientId))
        {
            Console.WriteLine("Код клиента должен быть натуральным числом.");
            return;
        }

        Console.Write("Цена доставки: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Цена должна быть числом.");
            return;
        }

        Console.Write(
@"Статус:
1. Выполнено
2. Не выполнено
Выберите статус: ");

        var statusChoice = Console.ReadLine();
        var status = statusChoice switch
        {
            "1" => "Выполнено",
            "2" => "Не выполнено",
            _ => null
        };

        if (status == null)
        {
            Console.WriteLine("Такого пункта нет. Введите число 1 или 2.");
            return;
        }

        try
        {
            _repository.AddOrder(new Order(0, date, clientId, price, status));
            Console.WriteLine("Заказ успешно добавлен.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Добавляет новое блюдо в меню.
    /// </summary>
    private static void AddMenuItem()
    {
        Console.Write("Название блюда: ");
        var dishName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(dishName))
        {
            Console.WriteLine("Название блюда не может быть пустым.");
            return;
        }

        Console.Write("Цена: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal dishPrice))
        {
            Console.WriteLine("Цена должна быть числом.");
            return;
        }

        if (dishPrice < 0)
        {
            Console.WriteLine("Цена не может быть отрицательной.");
            return;
        }

        _repository.AddMenuItem(new Menu(0, dishName, dishPrice));
        Console.WriteLine("Блюдо успешно добавлено в меню.");
    }

    /// <summary>
    /// Добавляет новую позицию в заказ.
    /// </summary>
    private static void AddOrderItem()
    {
        Console.Write("Код заказа: ");

        if (!int.TryParse(Console.ReadLine(), out int orderId))
        {
            Console.WriteLine("Код заказа должен быть натуральным числом.");
            return;
        }

        Console.Write("Код блюда: ");

        if (!int.TryParse(Console.ReadLine(), out int dishId))
        {
            Console.WriteLine("Код блюда должен быть натуральным числом.");
            return;
        }

        Console.Write("Количество: ");

        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Количество должно быть натуральным числом.");
            return;
        }

        if (quantity <= 0)
        {
            Console.WriteLine("Количество должно быть больше нуля.");
            return;
        }

        try
        {
            _repository.AddOrderItem(new OrderItem(0, orderId, dishId, quantity));
            Console.WriteLine("Позиция заказа успешно добавлена.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Выполняет запросы и отображает отчёты.
    /// </summary>
    private static void ExecuteQueries()
    {
        Console.Clear();
        Console.Write(
@"=== ПОЛУЧЕНИЕ ОТЧЕТОВ ===
1. Клиенты из Перми (одна таблица, возвращает перечень)
2. Невыполненные заказы с информацией о клиентах (две таблицы, возвращает перечень)
3. Общий доход от выполненных заказов (три таблицы, возвращает одно значение)
4. Среднее количество позиций в заказе (три таблицы, возвращает одно значение)
Выберите отчет: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("\n=== КЛИЕНТЫ ИЗ ПЕРМИ ===");

                var permClients = _repository.GetClientsFromPerm();

                if (!permClients.Any())
                {
                    Console.WriteLine("Клиенты из Перми не найдены.");
                }
                else
                {
                    foreach (var client in permClients)
                    {
                        Console.WriteLine(client);
                    }
                }

                break;

            case "2":
                Console.WriteLine("\n=== НЕВЫПОЛНЕННЫЕ ЗАКАЗЫ С ИНФОРМАЦИЕЙ О КЛИЕНТАХ ===");

                var orderInfos = _repository.GetOrdersWithClientInfo();

                if (!orderInfos.Any())
                {
                    Console.WriteLine("Невыполненные заказы не найдены.");
                }
                else
                {
                    foreach (var orderInfo in orderInfos)
                    {
                        Console.WriteLine(orderInfo);
                    }
                }

                break;

            case "3":
                decimal totalIncome = _repository.GetTotalDeliveredOrdersIncome();
                Console.WriteLine($"\nОБЩИЙ ДОХОД ОТ ВЫПОЛНЕННЫХ ЗАКАЗОВ: {totalIncome:F2} р.");
                break;

            case "4":
                double avgItems = _repository.GetAverageItemsPerOrder();
                Console.WriteLine($"\nСРЕДНЕЕ КОЛИЧЕСТВО ПОЗИЦИЙ В ЗАКАЗЕ: {avgItems:F2}");
                break;

            default:
                Console.WriteLine("Такого пункта нет. Выберите число от 1 до 4.");
                break;
        }
    }
}