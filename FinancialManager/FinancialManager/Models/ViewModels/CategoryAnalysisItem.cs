using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancialManager.Models.ViewModels
{
    /// <summary>
    /// Представляет элемент отчета "Анализ доходов и расходов по категориям".
    /// Содержит сводные данные по финансовым операциям в рамках одной категории.
    /// </summary>
    public class CategoryAnalysisItem
    {
        /// <summary>
        /// Название категории.
        /// </summary>
        [Display(Name = "Категория")]
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// Тип категории.
        /// </summary>
        [Display(Name = "Тип категории")]
        public string CategoryType { get; set; } = string.Empty;

        /// <summary>
        /// Общая сумма всех операций в данной категории за указанный период.
        /// </summary>
        [Display(Name = "Общая сумма")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Количество транзакций в данной категории за указанный период.
        /// </summary>
        [Display(Name = "Количество операций")]
        public int TransactionCount { get; set; }
    }
}