/// <summary>
/// Представляет информацию о клиенте пиццерии.
/// </summary>
internal class Client
{
    /// <summary>
    /// Получает или задаёт уникальный идентификатор клиента.
    /// </summary>
    public int ClientID { get; set; }

    /// <summary>
    /// Получает или задаёт фамилию клиента.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Получает или задаёт имя клиента.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Получает или задаёт отчество клиента.
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    /// Получает или задаёт город проживания клиента.
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// Инициализирует новый экземпляр класса Client.
    /// </summary>
    /// <param name="clientId">Уникальный идентификатор клиента.</param>
    /// <param name="lastName">Фамилия клиента.</param>
    /// <param name="name">Имя клиента.</param>
    /// <param name="patronymic">Отчество клиента.</param>
    /// <param name="city">Город проживания клиента.</param>
    public Client(int clientId, string lastName, string name, string patronymic, string city)
    {
        ClientID = clientId;
        LastName = lastName;
        Name = name;
        Patronymic = patronymic;
        City = city;
    }

    /// <summary>
    /// Возвращает строковое представление клиента.
    /// </summary>
    /// <returns>Строка с информацией о клиенте.</returns>
    public override string ToString()
    {
        return $"{this.ClientID}: {this.LastName} {this.Name} {this.Patronymic}, {this.City}";
    }
}
