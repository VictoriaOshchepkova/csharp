/// <summary>
/// Представляет информацию о блюде из меню.
/// </summary>
internal class Menu
{
    /// <summary>
    /// Получает или задаёт уникальный идентификатор блюда.
    /// </summary>
    internal int DishID { get; set; }

    /// <summary>
    /// Получает или задаёт название блюда.
    /// </summary>
    internal string Name { get; set; }

    /// <summary>
    /// Получает или задаёт цену блюда.
    /// </summary>
    internal decimal Price { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса Menu.
    /// </summary>
    /// <param name="dishId">Уникальный идентификатор блюда.</param>
    /// <param name="name">Название блюда.</param>
    /// <param name="price">Цена блюда.</param>
    internal Menu(int dishId, string name, decimal price)
    {
        DishID = dishId;
        Name = name;
        Price = price;
    }

    /// <summary>
    /// Возвращает строковое представление блюда.
    /// </summary>
    /// <returns>Строка с информацией о блюде.</returns>
    public override string ToString()
    {
        return $"{this.DishID}: {this.Name}, {this.Price} р.";
    }
}
