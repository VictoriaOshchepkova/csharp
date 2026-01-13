using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace FinancialManager.Models
{
    /// <summary>
     /// Представляет сущность пользователя.
     /// Хранит основные данные о пользователе и связанные с ним финансовые объекты.
     /// Сопоставляется с таблицей "Users" в базе данных.
     /// </summary>
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя.
        /// Автоматически генерируется базой данных при создании записи.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Адрес электронной почты пользователя.
        /// </summary>
        /// <remarks>
        /// Обязательное поле, должно быть валидным email-адресом.
        /// Максимальная длина - 100 символов.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Email» обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Неверный формат email")]
        [MaxLength(100, ErrorMessage = "Email не должен превышать 100 символов")]
        [Column("email")]
        public string Email { get; set; }

        /// <summary>
        /// Фамилия пользователя.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Максимальная длина - 20 символов.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Фамилия» обязательно для заполнения")]
        [MaxLength(20, ErrorMessage = "Фамилия не должна превышать 20 символов")]
        [Display(Name = "Фамилия")]
        [Column("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Максимальная длина - 20 символов.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Имя» обязательно для заполнения")]
        [MaxLength(20, ErrorMessage = "Имя не должно превышать 20 символов")]
        [Display(Name = "Имя")]
        [Column("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество пользователя.
        /// </summary>
        /// <remarks>
        /// Необязательное поле. Максимальная длина - 20 символов.
        /// </remarks>
        [MaxLength(20, ErrorMessage = "Отчество не должно превышать 20 символов")]
        [Display(Name = "Отчество")]
        [Column("patronymic")]
        public string? Patronymic { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Максимальная длина - 255 символов.
        /// </remarks>
        [Required(ErrorMessage = "Поле «Пароль» обязательно для заполнения")]
        [MaxLength(255, ErrorMessage = "Пароль не должен превышать 255 символов")]
        [Display(Name = "Пароль")]
        [Column("password")]
        public string Password { get; set; }

        /// <summary>
        /// Коллекция финансовых счетов, принадлежащих пользователю.
        /// </summary>
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

        /// <summary>
        /// Коллекция бюджетов, созданных пользователем.
        /// </summary>
        public virtual ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    }
}
