using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Cells;
public class Client
{
    public int ClientID { get; set; }
    public string LastName { get; set; }
    public string Name { get; set; }
    public string Patronymic { get; set; }
    public string City { get; set; }
    public Client(int clientId, string lastName, string name, string patronymic, string city)
    {
        ClientID = clientId;
        LastName = lastName;
        Name = name;
        Patronymic = patronymic;
        City = city;
    }
    public override string ToString()
    {
        return $"{ClientID}: {LastName} {Name} {Patronymic}, {City}";
    }
}
public class Order
{
    public int OrderID { get; set; }
    public DateTime Date { get; set; }
    public int ClientID { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; }
    public Order(int orderId, DateTime date, int clientId, decimal price, string status)
    {
        OrderID = orderId;
        Date = date;
        ClientID = clientId;
        Price = price;
        Status = status;
    }
    public override string ToString()
    {
        return $"{OrderID}: от {Date:dd.MM.yyyy}, клиент {ClientID}, стоимость доставки {Price} р., {Status}";
    }
}
public class OrderItem
{
    public int OrderItemID { get; set; }
    public int OrderID { get; set; }
    public int DishID { get; set; }
    public int Quantity { get; set; }
    public OrderItem(int orderItemId, int orderId, int dishId, int quantity)
    {
        OrderItemID = orderItemId;
        OrderID = orderId;
        DishID = dishId;
        Quantity = quantity;
    }
    public override string ToString()
    {
        return $"{OrderItemID}: заказ {OrderID}, блюдо {DishID}, {Quantity} шт.";
    }
}
public class Menu
{
    public int DishID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public Menu(int dishId, string name, decimal price)
    {
        DishID = dishId;
        Name = name;
        Price = price;
    }
    public override string ToString()
    {
        return $"{DishID}: {Name}, {Price} р.";
    }
}
