using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Cells;

class Program
{
    private static PizzaRepository repository = new PizzaRepository();

    static void Main(string[] args)
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
                        repository.LoadData();
                        ShowMainMenu();
                        break;
                    case "2":
                        repository.CreateTestDB();
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
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    static void ShowMainMenu()
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
                        repository.SaveToExcel();
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
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    static void ViewDatabase()
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
                foreach (var client in repository.Clients)
                {
                    Console.WriteLine(client);
                }
                break;
            case "2":
                Console.WriteLine("\n=== ЗАКАЗЫ ===");
                foreach (var order in repository.Orders)
                {
                    Console.WriteLine(order);
                }
                break;
            case "3":
                Console.WriteLine("\n=== СОСТАВ ЗАКАЗОВ ===");
                foreach (var item in repository.OrderItems)
                {
                    Console.WriteLine(item);
                }
                break;
            case "4":
                Console.WriteLine("\n=== МЕНЮ ===");
                foreach (var item in repository.Menu)
                {
                    Console.WriteLine(item);
                }
                break;
            default:
                Console.WriteLine("Такого пункта нет. Выберите число от 1 до 4.");
                break;
        }
    }

    static void DeleteItems()
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
                    Console.Write("Введите код клиента: ");
                    if (int.TryParse(Console.ReadLine(), out int clientId))
                    {
                        if (repository.RemoveClient(clientId))
                            Console.WriteLine("Клиент успешно удален.");
                        else
                            Console.WriteLine("Клиент с указанным кодом не найден.");
                    }
                    else
                    {
                        Console.WriteLine("Значение кода должно являться натуральным числом.");
                    }
                    break;
                case "2":
                    Console.Write("Введите код заказа: ");
                    if (int.TryParse(Console.ReadLine(), out int orderId))
                    {
                        if (repository.RemoveOrder(orderId))
                            Console.WriteLine("Заказ успешно удален.");
                        else
                            Console.WriteLine("Заказ с указанным кодом не найден.");
                    }
                    else
                    {
                        Console.WriteLine("Значение кода должно являться натуральным числом.");
                    }
                    break;
                case "3":
                    Console.Write("Введите код блюда: ");
                    if (int.TryParse(Console.ReadLine(), out int dishId))
                    {
                        if (repository.RemoveMenuItem(dishId))
                            Console.WriteLine("Блюдо успешно удалено из меню.");
                        else
                            Console.WriteLine("Блюдо с указанным кодом не найдено.");
                    }
                    else
                    {
                        Console.WriteLine("Значение кода должно являться натуральным числом.");
                    }
                    break;
                case "4":
                    Console.Write("Введите код позиции заказа: ");
                    if (int.TryParse(Console.ReadLine(), out int orderItemId))
                    {
                        if (repository.RemoveOrderItem(orderItemId))
                            Console.WriteLine("Позиция заказа успешно удалена.");
                        else
                            Console.WriteLine("Позиция заказа с указанным кодом не найдена.");
                    }
                    else
                    {
                        Console.WriteLine("Значение кода должно являться натуральным числом.");
                    }
                    break;
                default:
                    Console.WriteLine("Такого пункта нет. Введите число от 1 до 4.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting data: {ex.Message}");
        }
    }

    static void AddItems()
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
                    Console.Write("Фамилия: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Имя: ");
                    string name = Console.ReadLine();
                    Console.Write("Отчество: ");
                    string patronymic = Console.ReadLine();
                    Console.Write("Город: ");
                    string city = Console.ReadLine();

                    repository.AddClient(new Client(0, lastName, name, patronymic, $"г. {city}"));
                    Console.WriteLine("Клиент успешно добавлен.");
                    break;
                case "2":
                    Console.Write("Дата (дд.мм.гггг): ");
                    DateTime date = DateTime.Parse(Console.ReadLine());
                    Console.Write("Код клиента: ");
                    int clientId = int.Parse(Console.ReadLine());
                    Console.Write("Цена доставки: ");
                    decimal price = decimal.Parse(Console.ReadLine());
                    Console.Write(
@"Статус:
1. Выполнено
2. Не выполнено
Выберите статус: ");
                    string status = Console.ReadLine();
                    switch (status)
                    {
                        case "1":
                            status = "Выполнено";
                            try
                            {
                                repository.AddOrder(new Order(0, date, clientId, price, status));
                                Console.WriteLine("Заказ успешно добавлен.");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case "2":
                            status = "Не выполнено";
                            try
                            {
                                repository.AddOrder(new Order(0, date, clientId, price, status));
                                Console.WriteLine("Заказ успешно добавлен.");
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        default:
                            Console.WriteLine("Такого пункта нет. Введите число от 1 до 2.");
                            break;
                    }

                    break;
                case "3":
                    Console.Write("Название блюда: ");
                    string dishName = Console.ReadLine();
                    Console.Write("Цена: ");
                    decimal dishPrice = decimal.Parse(Console.ReadLine());

                    repository.AddMenuItem(new Menu(0, dishName, dishPrice));
                    Console.WriteLine("Блюдо успешно добавлено в меню.");
                    break;
                case "4":
                    Console.Write("Код заказа: ");
                    int orderId = int.Parse(Console.ReadLine());
                    Console.Write("Код блюда: ");
                    int dishId = int.Parse(Console.ReadLine());
                    Console.Write("Количество: ");
                    int quantity = int.Parse(Console.ReadLine());

                    try
                    {
                        repository.AddOrderItem(new OrderItem(0, orderId, dishId, quantity));
                        Console.WriteLine("Позиция заказа успешно добавлен.");
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                default:
                    Console.WriteLine("Такого пункта нет. Введите число от 1 до 4.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting data: {ex.Message}");
        }
    }

    static void ExecuteQueries()
    {
        Console.Clear();
        Console.Write(
@"=== ПОЛУЧЕНИЕ ОТЧЕТОВ ===
1. Клиенты из Перми
2. Невыполненные заказы с информацией о клиентах
3. Общий доход от выполненных заказов (вкл. стоимость доставки)
4. Среднее количество позиций в заказе
Выберите отчет: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("\n=== КЛИЕНТЫ ИЗ ПЕРМИ ===");
                foreach (var client in repository.GetClientsFromPerm())
                {
                    Console.WriteLine(client);
                }
                break;
            case "2":
                Console.WriteLine("\n=== НЕВЫПОЛНЕННЫЕ ЗАКАЗЫ С ИНФОРМАЦИЕЙ О КЛИЕНТАХ ===");
                foreach (var orderInfo in repository.GetOrdersWithClientInfo())
                {
                    Console.WriteLine(orderInfo);
                }
                break;
            case "3":
                decimal totalIncome = repository.GetTotalDeliveredOrdersIncome();
                Console.WriteLine($"\nОБЩИЙ ДОХОД ОТ ВЫПОЛНЕННЫХ ЗАКАЗОВ: {totalIncome} р.");
                break;
            case "4":
                double avgItems = repository.GetAverageItemsPerOrder();
                Console.WriteLine($"\nСРЕДНЕЕ КОЛИЧЕСТВО ПОЗИЦИЙ В ЗАКАЗЕ: {avgItems:F2}");
                break;
            default:
                Console.WriteLine("Такого пункта нет. Выберите число от 1 до 4.");
                break;
        }
    }
}