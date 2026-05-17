/// <summary>
/// Представляет информацию о заказе в пиццерии.
/// </summary>
internal class Order
{
    /// <summary>
    /// Получает или задаёт уникальный идентификатор заказа.
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// Получает или задаёт дату заказа.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Получает или задаёт идентификатор клиента, сделавшего заказ.
    /// </summary>
    public int ClientID { get; set; }

    /// <summary>
    /// Получает или задаёт стоимость доставки.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Получает или задаёт статус выполнения заказа.
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса Order.
    /// </summary>
    /// <param name="orderId">Уникальный идентификатор заказа.</param>
    /// <param name="date">Дата заказа.</param>
    /// <param name="clientId">Идентификатор клиента.</param>
    /// <param name="price">Стоимость доставки.</param>
    /// <param name="status">Статус заказа.</param>
    public Order(int orderId, DateTime date, int clientId, decimal price, string status)
    {
        OrderID = orderId;
        Date = date;
        ClientID = clientId;
        Price = price;
        Status = status;
    }

    /// <summary>
    /// Возвращает строковое представление заказа.
    /// </summary>
    /// <returns>Строка с информацией о заказе.</returns>
    public override string ToString()
    {
        return $"{OrderID}: от {Date:dd.MM.yyyy}, клиент {ClientID}, стоимость доставки {Price} р., {Status}";
    }
}
