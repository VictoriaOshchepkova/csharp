using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancialManager.Models.ViewModels
{
    /// <summary>
    /// Представляет элемент отчета "История операций".
    /// Содержит информацию об одной финансовой операции для отображения в отчетах.
    /// </summary>
    public class TransactionHistoryItem
    {
        /// <summary>
        /// Дата совершения транзакции.
        /// </summary>
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Время совершения транзакции.
        /// </summary>
        [Display(Name = "Время")]
        [DataType(DataType.Time)]
        public TimeSpan TransactionTime { get; set; }

        /// <summary>
        /// Название счета, на котором была совершена транзакция.
        /// </summary>
        [Display(Name = "Счет")]
        public string AccountName { get; set; } = string.Empty;

        /// <summary>
        /// Название категории транзакции.
        /// </summary>
        [Display(Name = "Категория")]
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// Тип операции.
        /// </summary>
        [Display(Name = "Тип операции")]
        public string OperationType { get; set; } = string.Empty;

        /// <summary>
        /// Сумма транзакции.
        /// </summary>
        [Display(Name = "Сумма")]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Описание транзакции.
        /// </summary>
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        /// <summary>
        /// Флаг, указывающий является ли транзакция доходом.
        /// </summary>
        public bool IsIncome { get; set; }
    }
}