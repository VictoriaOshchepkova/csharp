using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancialManager.Models.ViewModels
{
    /// <summary>
    /// ViewModel для формирования и отображения отчета "История операций".
    /// Содержит параметры фильтрации и результаты отчета.
    /// </summary>
    public class TransactionHistoryViewModel
    {
        /// <summary>
        /// Начальная дата периода для фильтрации транзакций.
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию: 1 месяц назад от текущей даты.
        /// </remarks>
        [Display(Name = "Начальная дата")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);

        /// <summary>
        /// Конечная дата периода для фильтрации транзакций.
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию: текущая дата.
        /// </remarks>
        [Display(Name = "Конечная дата")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Идентификатор счета для фильтрации транзакций.
        /// </summary>
        [Display(Name = "Счет")]
        public int? AccountId { get; set; }

        /// <summary>
        /// Идентификатор категории для фильтрации транзакций.
        /// </summary>
        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Список счетов для выбора в выпадающем списке.
        /// </summary>
        public List<SelectListItem> Accounts { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Список категорий для выбора в выпадающем списке.
        /// </summary>
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Список отфильтрованных транзакций.
        /// </summary>
        public List<TransactionHistoryItem> Transactions { get; set; } = new List<TransactionHistoryItem>();
    }
}