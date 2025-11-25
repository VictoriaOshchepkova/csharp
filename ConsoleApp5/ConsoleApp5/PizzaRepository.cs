using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Cells;

public class PizzaRepository
{
    public List<Client> Clients { get; set; }
    public List<Order> Orders { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public List<Menu> Menu { get; set; }

    public PizzaRepository()
    {
        Clients = new List<Client>();
        Orders = new List<Order>();
        OrderItems = new List<OrderItem>();
        Menu = new List<Menu>();
    }

    public void LoadData()
    {
        try
        {
            Workbook workbook = new Workbook("LR5-var8.xls");

            Worksheet clientsSheet = workbook.Worksheets["Клиенты"];
            for (int row = 1; row <= clientsSheet.Cells.MaxDataRow; row++)
            {
                if (int.TryParse(clientsSheet.Cells[row, 0].Value?.ToString(), out int clientId))
                {
                    string lastName = clientsSheet.Cells[row, 1].Value?.ToString() ?? "";
                    string name = clientsSheet.Cells[row, 2].Value?.ToString() ?? "";
                    string patronymic = clientsSheet.Cells[row, 3].Value?.ToString() ?? "";
                    string city = clientsSheet.Cells[row, 4].Value?.ToString() ?? "";

                    Clients.Add(new Client(clientId, lastName, name, patronymic, city));
                }
            }

            Worksheet ordersSheet = workbook.Worksheets["Заказы"];
            for (int row = 1; row <= ordersSheet.Cells.MaxDataRow; row++)
            {
                if (int.TryParse(ordersSheet.Cells[row, 0].Value?.ToString(), out int orderId))
                {
                    DateTime date = DateTime.Parse(ordersSheet.Cells[row, 1].Value?.ToString() ?? DateTime.Now.ToString());
                    int clientId = int.Parse(ordersSheet.Cells[row, 2].Value?.ToString() ?? "0");

                    string priceStr = (ordersSheet.Cells[row, 3].Value?.ToString() ?? "0").Split(' ')[0];
                    decimal price = 0;
                    decimal.TryParse(priceStr, out price);

                    string status = ordersSheet.Cells[row, 4].Value?.ToString() ?? "";

                    Orders.Add(new Order(orderId, date, clientId, price, status));
                }
            }

            Worksheet orderItemsSheet = workbook.Worksheets["Состав заказов"];
            for (int row = 1; row <= orderItemsSheet.Cells.MaxDataRow; row++)
            {
                if (int.TryParse(orderItemsSheet.Cells[row, 0].Value?.ToString(), out int orderItemId))
                {
                    string orderIdStr = (orderItemsSheet.Cells[row, 1].Value?.ToString() ?? "0");
                    int orderId = 0;
                    int.TryParse(orderIdStr, out orderId);

                    string dishIdStr = (orderItemsSheet.Cells[row, 2].Value?.ToString() ?? "0");
                    int dishId = 0;
                    int.TryParse(dishIdStr, out dishId);

                    string quantityStr = (orderItemsSheet.Cells[row, 3].Value?.ToString() ?? "0");
                    int quantity = 0;
                    int.TryParse(quantityStr, out quantity);

                    OrderItems.Add(new OrderItem(orderItemId, orderId, dishId, quantity));
                }
            }

            Worksheet menuSheet = workbook.Worksheets["Меню"];
            for (int row = 1; row <= menuSheet.Cells.MaxDataRow; row++)
            {
                if (int.TryParse(menuSheet.Cells[row, 0].Value?.ToString(), out int dishId))
                {
                    string name = menuSheet.Cells[row, 1].Value?.ToString() ?? "";

                    string priceStr = menuSheet.Cells[row, 2].Value?.ToString() ?? "0";
                    decimal price = 0;
                    decimal.TryParse(priceStr, out price);

                    Menu.Add(new Menu(dishId, name, price));
                }
            }

            Console.WriteLine("Данные успешно выгружены из базы данных.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading data: {ex.Message}");
        }
    }

    public void CreateTestDB()
    {
        Clients.Add(new Client(1, "Иванов", "Иван", "Иванович", "г. Пермь"));
        Clients.Add(new Client(2, "Петров", "Петр", "Петрович", "г. Москва"));
        Clients.Add(new Client(3, "Сидорова", "Мария", "Сергеевна", "г. Пермь"));
        Clients.Add(new Client(4, "Кузнецов", "Алексей", "Владимирович", "г. Екатеринбург"));
        Clients.Add(new Client(5, "Смирнова", "Ольга", "Дмитриевна", "г. Пермь"));

        Menu.Add(new Menu(1, "Пепперони", 450));
        Menu.Add(new Menu(2, "Маргарита", 380));
        Menu.Add(new Menu(3, "Гавайская", 420));
        Menu.Add(new Menu(4, "Четыре сыра", 480));
        Menu.Add(new Menu(5, "Мясная", 520));

        Orders.Add(new Order(1, new DateTime(2025, 11, 20), 1, 150, "Выполнено"));
        Orders.Add(new Order(2, new DateTime(2025, 11, 22), 3, 0, "Не выполнено"));
        Orders.Add(new Order(3, new DateTime(2025, 11, 23), 5, 180, "Выполнено"));
        Orders.Add(new Order(4, new DateTime(2025, 11, 24), 2, 0, "Не выполнено"));
        Orders.Add(new Order(5, new DateTime(2025, 11, 18), 4, 170, "Выполнено"));

        OrderItems.Add(new OrderItem(1, 1, 1, 2));
        OrderItems.Add(new OrderItem(2, 1, 3, 1));
        OrderItems.Add(new OrderItem(3, 2, 2, 1));
        OrderItems.Add(new OrderItem(4, 3, 5, 1));
        OrderItems.Add(new OrderItem(5, 4, 4, 1));
        OrderItems.Add(new OrderItem(6, 5, 1, 1));

        Console.WriteLine("Тестовая база данных успешно создана.");
    }

    public List<Client> GetClientsFromPerm()
    {
        return (from client in Clients
                where client.City == "г. Пермь"
                select client)
                .ToList();
    }

    public List<string> GetOrdersWithClientInfo()
    {
        return (from order in Orders
                join client in Clients on order.ClientID equals client.ClientID
                where order.Status == "Не выполнено"
                select $"Заказ {order.OrderID} от {order.Date:dd.MM.yyyy} - {client.LastName} {client.Name}, {client.City}")
                .ToList();
    }

    public decimal GetTotalDeliveredOrdersIncome()
    {
        return (from order in Orders
                join orderItem in OrderItems on order.OrderID equals orderItem.OrderID
                join dish in Menu on orderItem.DishID equals dish.DishID
                where order.Status == "Выполнено"
                select orderItem.Quantity * dish.Price + order.Price)
                .Sum();
    }

    public double GetAverageItemsPerOrder()
    {
        return (from order in Orders
                join orderItem in OrderItems on order.OrderID equals orderItem.OrderID
                join dish in Menu on orderItem.DishID equals dish.DishID
                group orderItem by order.OrderID into g
                select g.Sum(oi => oi.Quantity))
                .DefaultIfEmpty()
                .Average();
    }

    public bool RemoveClient(int clientId)
    {
        var client = (from c in Clients where c.ClientID == clientId select c).FirstOrDefault();
        if (client != null)
        {
            var ordersToRemove = (from o in Orders where o.ClientID == clientId select o).ToList();
            foreach (var order in ordersToRemove)
            {
                var orderItemsToRemove = (from oi in OrderItems where oi.OrderID == order.OrderID select oi).ToList();
                foreach (var item in orderItemsToRemove)
                {
                    OrderItems.Remove(item);
                }
                Orders.Remove(order);
            }

            Clients.Remove(client);
            return true;
        }
        return false;
    }

    public void AddClient(Client client)
    {
        if (Clients.Any())
        {
            client.ClientID = (from c in Clients select c.ClientID).Max() +1;
        }
        else
        {
            client.ClientID = 1;
        }
        Clients.Add(client);
    }

    public bool RemoveOrder(int orderId)
    {
        var order = (from o in Orders where o.OrderID == orderId select o).FirstOrDefault();
        if (order != null)
        {
            Orders.Remove(order);
            var orderItemsToRemove = (from oi in OrderItems where oi.OrderID == orderId select oi).ToList();
            foreach (var item in orderItemsToRemove)
            {
                OrderItems.Remove(item);
            }
            return true;
        }
        return false;
    }

    public void AddOrder(Order order)
    {
        var client = (from c in Clients where c.ClientID == order.ClientID select c).FirstOrDefault();
        if (client == null)
        {
            throw new ArgumentException($"Клиент с кодом {order.ClientID} не существует.");
        }
        if (Orders.Any())
        {
            order.OrderID = (from o in Orders select o.OrderID).Max() + 1;
        }
        else
        {
            order.OrderID = 1;
        }
        Orders.Add(order);
    }

    public bool RemoveMenuItem(int dishId)
    {
        var menuItem = (from m in Menu where m.DishID == dishId select m).FirstOrDefault();
        if (menuItem != null)
        {
            var orderItemsToRemove = (from oi in OrderItems where oi.DishID == dishId select oi).ToList();
            foreach (var item in orderItemsToRemove)
            {
                OrderItems.Remove(item);
            }

            Menu.Remove(menuItem);
            return true;
        }
        return false;
    }

    public void AddMenuItem(Menu menuItem)
    {
        if (Menu.Any())
        {
            menuItem.DishID = (from m in Menu select m.DishID).Max() + 1;
        }
        else
        {
            menuItem.DishID = 1;
        }
        Menu.Add(menuItem);
    }

    public bool RemoveOrderItem(int orderItemId)
    {
        var orderItem = (from oi in OrderItems where oi.OrderItemID == orderItemId select oi).FirstOrDefault();
        if (orderItem != null)
        {
            OrderItems.Remove(orderItem);
            return true;
        }
        return false;
    }

    public void AddOrderItem(OrderItem orderItem)
    {
        var order = (from o in Orders where o.OrderID == orderItem.OrderID select o).FirstOrDefault();
        if (order == null)
        {
            throw new ArgumentException($"Заказ с кодом {orderItem.OrderID} не существует.");
        }

        var menuItem = (from m in Menu where m.DishID == orderItem.DishID select m).FirstOrDefault();
        if (menuItem == null)
        {
            throw new ArgumentException($"Блюдо с кодом {orderItem.DishID} не существует.");
        }
        if (OrderItems.Any())
        {
            orderItem.OrderItemID = (from oi in OrderItems select oi.OrderItemID).Max() + 1;
        }
        else
        {
            orderItem.OrderItemID = 1;
        }
        OrderItems.Add(orderItem);
    }

    public void SaveToExcel()
    {
        try
        {
            Workbook workbook = new Workbook();

            Worksheet clientsSheet = workbook.Worksheets[0];
            clientsSheet.Name = "Клиенты";
            clientsSheet.Cells[0, 0].PutValue("Код клиента");
            clientsSheet.Cells[0, 1].PutValue("Фамилия");
            clientsSheet.Cells[0, 2].PutValue("Имя");
            clientsSheet.Cells[0, 3].PutValue("Отчество");
            clientsSheet.Cells[0, 4].PutValue("Место жительства");

            int row = 1;
            foreach (var client in Clients)
            {
                clientsSheet.Cells[row, 0].PutValue(client.ClientID);
                clientsSheet.Cells[row, 1].PutValue(client.LastName);
                clientsSheet.Cells[row, 2].PutValue(client.Name);
                clientsSheet.Cells[row, 3].PutValue(client.Patronymic);
                clientsSheet.Cells[row, 4].PutValue(client.City);
                ++row;
            }

            Worksheet ordersSheet = workbook.Worksheets.Add("Заказы");
            ordersSheet.Cells[0, 0].PutValue("Код заказа");
            ordersSheet.Cells[0, 1].PutValue("Дата");
            ordersSheet.Cells[0, 2].PutValue("Код клиента");
            ordersSheet.Cells[0, 3].PutValue("Цена доставки");
            ordersSheet.Cells[0, 4].PutValue("Статус доставки");

            row = 1;
            foreach (var order in Orders)
            {
                ordersSheet.Cells[row, 0].PutValue(order.OrderID);
                ordersSheet.Cells[row, 1].PutValue(order.Date.ToString("dd.MM.yyyy"));
                ordersSheet.Cells[row, 2].PutValue(order.ClientID);
                ordersSheet.Cells[row, 3].PutValue($"{order.Price} р.");
                ordersSheet.Cells[row, 4].PutValue(order.Status);
                ++row;
            }

            Worksheet orderItemsSheet = workbook.Worksheets.Add("Состав заказов");
            orderItemsSheet.Cells[0, 0].PutValue("Код");
            orderItemsSheet.Cells[0, 1].PutValue("Код заказа");
            orderItemsSheet.Cells[0, 2].PutValue("Код блюда");
            orderItemsSheet.Cells[0, 3].PutValue("Количество");

            row = 1;
            foreach (var item in OrderItems)
            {
                orderItemsSheet.Cells[row, 0].PutValue(item.OrderItemID);
                orderItemsSheet.Cells[row, 1].PutValue(item.OrderID);
                orderItemsSheet.Cells[row, 2].PutValue(item.DishID);
                orderItemsSheet.Cells[row, 3].PutValue(item.Quantity);
                ++row;
            }

            Worksheet menuSheet = workbook.Worksheets.Add("Меню");
            menuSheet.Cells[0, 0].PutValue("Код блюда");
            menuSheet.Cells[0, 1].PutValue("Название");
            menuSheet.Cells[0, 2].PutValue("Цена");

            row = 1;
            foreach (var item in Menu)
            {
                menuSheet.Cells[row, 0].PutValue(item.DishID);
                menuSheet.Cells[row, 1].PutValue(item.Name);
                menuSheet.Cells[row, 2].PutValue($"{item.Price} р.");
                ++row;
            }

            workbook.Save("LR5-var8_updated.xls");
            Console.WriteLine($"Данные успешно сохранены в файл 'LR5 - var8_updated.xls'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
            throw;
        }
    }
}