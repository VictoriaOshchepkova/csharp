using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialManager.Models
{
    /// <summary>
    /// Представляет сущность категории для классификации финансовых операций.
    /// Сопоставляется с таблицей "Categories" в базе данных.
    /// </summary>
    [Table("Categories")]
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор категории.
        /// Автоматически генерируется базой данных при создании записи.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        [Column("category_id")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Максимальная длина - 100 символов.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Название категории» пользователя» обязательно для заполнения")]
        [MaxLength(100)]
        [Display(Name = "Название категории")]
        [Column("category_name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// Тип категории, определяющий характер операций (например: "Доход", "Расход", "Перевод (пополнение)", "Перевод (списание)".
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Максимальная длина - 50 символов.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Тип категории» обязательно для заполнения")]
        [MaxLength(50)]
        [Display(Name = "Тип категории")]
        [Column("category_type")]
        public string CategoryType { get; set; }

        /// <summary>
        /// Коллекция транзакций, связанных с данной категорией.
        /// </summary>
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        
        /// <summary>
        /// Коллекция бюджетов, в которых используется данная категория.
        /// </summary>
        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    }
}
