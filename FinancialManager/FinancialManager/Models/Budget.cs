using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialManager.Models
{
    /// <summary>
    /// Представляет сущность бюджета для планирования финансовых расходов.
    /// Сопоставляется с таблицей "Budgets" в базе данных.
    /// </summary>
    [Table("Budgets")]
    public class Budget
    {
        /// <summary>
        /// Уникальный идентификатор бюджета.
        /// Автоматически генерируется базой данных при создании записи.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        [Column("budget_id")]
        public int BudgetId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего бюджет.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Внешний ключ для связи с таблицей Users.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Email пользователя» обязательно для заполнения")]
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор категории, для которой устанавливается бюджет.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Внешний ключ для связи с таблицей Categories.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Категория» обязательно для заполнения")]
        [Column("category")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Дата начала бюджетного периода.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Хранится в формате DATE в базе данных.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Начало периода» обязательно для заполнения")]
        [DataType(DataType.Date)]
        [Display(Name = "Начало периода")]
        [Column("start_date", TypeName = "date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Планируемая сумма бюджета.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Хранится в формате decimal(15,2) в базе данных.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Планируемая сумма» обязательно для заполнения")]
        [Column("planned_amount", TypeName = "decimal(15,2)")]
        [Display(Name = "Планируемая сумма")]
        [Range(0, 9999999999999.99, ErrorMessage = "Сумма должна быть в диапазоне от 0 до 9 999 999 999 999.99")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Введите корректное число (например: 1000.50)")]
        public decimal PlannedAmount { get; set; }

        /// <summary>
        /// Дата окончания бюджетного периода.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Хранится в формате DATE в базе данных.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Конец периода» обязательно для заполнения")]
        [DataType(DataType.Date)]
        [Display(Name = "Конец периода")]
        [Column("end_date", TypeName = "date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Навигационное свойство для пользователя-владельца бюджета.
        /// </summary>
        /// <remarks>
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        /// <summary>
        /// Навигационное свойство для категории бюджета.
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}