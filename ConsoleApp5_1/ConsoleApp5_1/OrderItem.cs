/// <summary>
/// Представляет информацию о позиции в заказе (блюдо и количество).
/// </summary>
internal class OrderItem
{
    /// <summary>
    /// Получает или задаёт уникальный идентификатор позиции заказа.
    /// </summary>
    internal int OrderItemID { get; set; }

    /// <summary>
    /// Получает или задаёт идентификатор заказа.
    /// </summary>
    internal int OrderID { get; set; }

    /// <summary>
    /// Получает или задаёт идентификатор блюда.
    /// </summary>
    internal int DishID { get; set; }

    /// <summary>
    /// Получает или задаёт количество блюд.
    /// </summary>
    internal int Quantity { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса OrderItem.
    /// </summary>
    /// <param name="orderItemId">Уникальный идентификатор позиции.</param>
    /// <param name="orderId">Идентификатор заказа.</param>
    /// <param name="dishId">Идентификатор блюда.</param>
    /// <param name="quantity">Количество.</param>
    internal OrderItem(int orderItemId, int orderId, int dishId, int quantity)
    {
        OrderItemID = orderItemId;
        OrderID = orderId;
        DishID = dishId;
        Quantity = quantity;
    }

    /// <summary>
    /// Возвращает строковое представление позиции заказа.
    /// </summary>
    /// <returns>Строка с информацией о позиции.</returns>
    public override string ToString()
    {
        return $"{this.OrderItemID}: заказ {this.OrderID}, блюдо {this.DishID}, {this.Quantity} шт.";
    }
}
