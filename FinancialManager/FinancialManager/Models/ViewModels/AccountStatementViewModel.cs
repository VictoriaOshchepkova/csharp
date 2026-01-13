using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancialManager.Models.ViewModels
{
    /// <summary>
    /// ViewModel для формирования и отображения отчета "Ведомость по счетам".
    /// Содержит параметры фильтрации и результаты отчета.
    /// </summary>
    public class AccountStatementViewModel
    {
        /// <summary>
        /// Идентификатор пользователя, для которого формируется отчет.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Значение выбирается из выпадающего списка Users.
        /// </remarks>
        [Required(ErrorMessage = "Выберите пользователя")]
        [Display(Name = "Пользователь")]
        public int UserId { get; set; }

        /// <summary>
        /// Начальная дата периода для формирования отчета.
        /// </summary>
        /// <remarks>
        /// Обязательное поле. Значение по умолчанию: 1 месяц назад от текущей даты.
        /// </remarks>
        [Required(ErrorMessage = "Введите начальную дату")]
        [Display(Name = "Начальная дата")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1);

        /// <summary>
        /// Конечная дата периода для формирования выписки.
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
        /// Список элементов отчета "Ведомость по счетам"..
        /// Содержит сводные данные по финансовым операциям на одном счете за указанный период.
        /// </summary>
        public List<AccountStatementItem>? Accounts { get; set; }

        /// <summary>
        /// Общий начальный баланс по всем счетам.
        /// </summary>
        public decimal TotalInitialBalance
        {
            get
            {
                return Accounts?.Sum(a => a.InitialBalance) ?? 0;
            }
        }

        /// <summary>
        /// Общий приход по всем счетам.
        /// </summary>
        public decimal TotalIncome
        {
            get
            {
                return Accounts?.Sum(a => a.TotalIncome) ?? 0;
            }
        }

        /// <summary>
        /// Общий расход по всем счетам.
        /// </summary>
        public decimal TotalExpense
        {
            get
            {
                return Accounts?.Sum(a => a.TotalExpense) ?? 0;
            }
        }

        /// <summary>
        /// Общий конечный баланс по всем счетам.
        /// </summary>
        public decimal TotalFinalBalance
        {
            get
            {
                return Accounts?.Sum(a => a.FinalBalance) ?? 0;
            }
        }
    }
}