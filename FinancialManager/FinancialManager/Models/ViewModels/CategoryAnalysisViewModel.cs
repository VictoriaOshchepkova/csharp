using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancialManager.Models.ViewModels
{
    /// <summary>
    /// ViewModel для анализа финансовых операций по категориям.
    /// Содержит параметры для группировки транзакций по категориям и результаты анализа.
    /// </summary>
    public class CategoryAnalysisViewModel
    {
        /// <summary>
        /// Идентификатор пользователя, для которого проводится анализ.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Значение выбирается из выпадающего списка Users.
        /// </remarks>
        [Required(ErrorMessage = "Выберите пользователя")]
        [Display(Name = "Пользователь")]
        public int UserId { get; set; }

        /// <summary>
        /// Начальная дата периода для анализа.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Значение по умолчанию: 1 месяц назад от текущей даты.
        /// </remarks>
        [Required(ErrorMessage = "Введите начальную дату")]
        [Display(Name = "Начальная дата")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);

        /// <summary>
        /// Конечная дата периода для анализа.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Значение по умолчанию: текущая дата.
        /// </remarks>
        [Required(ErrorMessage = "Введите конечную дату")]
        [Display(Name = "Конечная дата")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Email пользователя (только для отображения).
        /// Заполняется на основе выбранного UserId для удобства пользователя.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Список пользователей для выбора в выпадающем списке.
        /// </summary>
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Список элементов отчета "Анализ доходов и расходов по категориям".
        /// Содержит сводные данные по финансовым операциям в рамках одной категории.
        /// </summary>
        public List<CategoryAnalysisItem> Categories { get; set; } = new List<CategoryAnalysisItem>();

        /// <summary>
        /// Общая сумма всех операций за период.
        /// </summary>
        public decimal TotalAmount
        {
            get
            {
                return Categories?.Sum(c => c.TotalAmount) ?? 0;
            }
        }
    }
}