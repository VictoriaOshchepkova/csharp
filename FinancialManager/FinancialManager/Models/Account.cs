using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialManager.Models
{
    /// <summary>
    /// Представляет сущность финансового счета пользователя.
    /// Хранит информацию о денежных средствах, их типе и связанных транзакциях.
    /// Сопоставляется с таблицей "Accounts" в базе данных.
    /// </summary>
    [Table("Accounts")]
    public class Account
    {
        /// <summary>
        /// Уникальный идентификатор счета.
        /// Автоматически генерируется базой данных при создании записи.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        [Column("account_id")]
        public int AccountId { get; set; }

        /// <summary>
        /// Идентификатор пользователя-владельца счета.
        /// Внешний ключ для связи с таблицей Users.
        /// </summary>
        /// <remarks>
        /// Обязательное поле.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Email» пользователя» обязательно для заполнения")]
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Название счета.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Максимальная длина - 100 символов.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Название счета» обязательно для заполнения")]
        [MaxLength(100, ErrorMessage = "Название счета не должен превышать 100 символов")]
        [Display(Name = "Название счета")]
        [Column("account_name")]
        public string AccountName { get; set; }

        /// <summary>
        /// Тип счета (например: "Дебетовый", "Кредитный", "Накопительный", "Наличный").
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Максимальная длина - 50 символов.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Тип счета» обязательно для заполнения")]
        [MaxLength(50, ErrorMessage = "Тип счета не должен превышать 50 символов")]
        [Display(Name = "Тип счета")]
        [Column("account_type")]
        public string AccountType { get; set; }

        /// <summary>
        /// Начальный баланс счета.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Значение по умолчанию: 0.00.
        /// Хранится в формате decimal(15,2) в базе данных.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Баланс» обязательно для заполнения")]
        [Column("balance", TypeName = "decimal(15,2)")]
        [Display(Name = "Баланс")]
        [Range(0, 9999999999999.99, ErrorMessage = "Баланс должен быть в диапазоне от 0 до 9 999 999 999 999.99")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Введите корректное число (например: 1000.50)")]
        public decimal? Balance { get; set; } = 0.00m;

        /// <summary>
        /// Кредитный лимит для кредитных счетов.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Значение по умолчанию: 0.00.
        /// Хранится в формате decimal(15,2) в базе данных.
        /// Для дебетовых счетов равно 0.00.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Кредитный лимит» обязательно для заполнения")]
        [Column("credit_limit", TypeName = "decimal(15,2)")]
        [Display(Name = "Кредитный лимит")]
        [Range(0, 9999999999999.99, ErrorMessage = "Кредитный лимит должен быть в диапазоне от 0 до 9 999 999 999 999.99")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Введите корректное число (например: 1000.50)")]
        public decimal? CreditLimit { get; set; } = 0.00m;

        /// <summary>
        /// Навигационное свойство для пользователя-владельца счета.
        /// Обеспечивает доступ к объекту User, связанному с данным счетом.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        /// <summary>
        /// Коллекция транзакций, связанных с данным счетом.
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
