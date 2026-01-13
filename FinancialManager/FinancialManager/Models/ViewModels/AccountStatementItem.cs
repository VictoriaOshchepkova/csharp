using System.ComponentModel.DataAnnotations;

namespace FinancialManager.Models.ViewModels
{
    /// <summary>
    /// Представляет элемент отчета "Ведомость по счетам".
    /// Содержит сводные данные по финансовым операциям на одном счете за указанный период.
    /// </summary>
    public class AccountStatementItem
    {
        /// <summary>
        /// Название счета.
        /// </summary>
        [Display(Name = "Название счета")]
        public string AccountName { get; set; } = string.Empty;

        /// <summary>
        /// Начальный баланс на счете на дату начала периода.
        /// </summary>
        [Display(Name = "Начальный баланс")]
        [DataType(DataType.Currency)]
        public decimal InitialBalance { get; set; }

        /// <summary>
        /// Общая сумма доходов на счет за указанный период.
        /// </summary>
        [Display(Name = "Общий приход")]
        [DataType(DataType.Currency)]
        public decimal TotalIncome { get; set; }

        /// <summary>
        /// Общая сумма расходов со счета за указанный период.
        /// </summary>
        [Display(Name = "Общий расход")]
        [DataType(DataType.Currency)]
        public decimal TotalExpense { get; set; }

        /// <summary>
        /// Конечный баланс на счете на дату окончания периода.
        /// </summary>
        [Display(Name = "Конечный баланс")]
        [DataType(DataType.Currency)]
        public decimal FinalBalance { get; set; }
    }
}