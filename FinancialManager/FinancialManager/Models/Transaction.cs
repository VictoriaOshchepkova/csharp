using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialManager.Models
{
    /// <summary>
    /// Представляет сущность финансовой транзакции.
    /// Хранит информацию о денежных операциях.
    /// Сопоставляется с таблицей "Transactions" в базе данных.
    /// </summary>
    [Table("Transactions")]
    public class Transaction
    {
        /// <summary>
        /// Уникальный идентификатор транзакции.
        /// Автоматически генерируется базой данных при создании записи.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        [Column("transaction_id")]
        public int TransactionId { get; set; }

        /// <summary>
        /// Идентификатор счета, к которому относится транзакция.
        /// Внешний ключ для связи с таблицей Accounts.
        /// </summary>
        /// <remarks>
        /// Обязательное поле.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Счет» обязательно для заполнения")]
        [Display(Name = "Счет")]
        [Column("account")]
        public int? AccountId { get; set; }

        /// <summary>
        /// Дата совершения транзакции.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Хранится в формате DATE в базе данных.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Дата» обязательно для заполнения")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата")]
        [Column("transaction_date", TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Время совершения транзакции.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Хранится в формате TIME в базе данных.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Время» обязательно для заполнения")]
        [DataType(DataType.Time)]
        [Display(Name = "Время")]
        [Column("transaction_time", TypeName = "time")]
        public TimeSpan TransactionTime { get; set; }

        /// <summary>
        /// Идентификатор категории транзакции.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Внешний ключ для связи с таблицей Categories.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Категория» обязательно для заполнения")]
        [Display(Name = "Категория")]
        [Column("category")]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Сумма транзакции.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. 
        /// Хранится в формате decimal(15,2) в базе данных.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Сумма» обязательно для заполнения")]
        [Column("amount", TypeName = "decimal(15,2)")]
        [Display(Name = "Сумма")]
        [Range(0, 9999999999999.99, ErrorMessage = "Сумма должна быть в диапазоне от 0 до 9 999 999 999 999.99")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Введите корректное число (например: 1000.50)")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Описание транзакции.
        /// </summary>
        /// <remarks>
        /// Необязательное поле.
        /// </remarks>
        [Column("description")]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        /// <summary>
        /// Навигационное свойство для счета, к которому относится транзакция.
        /// </summary>
        [ForeignKey("AccountId")]
        public virtual Account? Account { get; set; }

        /// <summary>
        /// Навигационное свойство для категории транзакции.
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}