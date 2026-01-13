using FinancialManager.Context;
using FinancialManager.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FinancialManager.Controllers
{
    /// <summary>
    /// Контроллер для формирования финансовых отчетов.
    /// Предоставляет методы для создания различных типов отчетов: 
    /// "Ведомость по счетам", "История операций", "Анализ доходов и расходов по категориям"
    /// </summary>
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера, принимающий контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных для работы с финансовыми данными.</param>
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reports/AccountStatement
        /// <summary>
        /// Отображает форму для формирования отчета "Ведомость по счетам".
        /// Загружает список пользователей для выбора.
        /// </summary>
        /// <returns>Представление с формой для создания отчета.</returns>
        public IActionResult AccountStatement()
        {
            var viewModel = new AccountStatementViewModel
            {
                Users = _context.Users
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserId.ToString(),
                        Text = u.Email
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        // POST: Reports/AccountStatement
        /// <summary>
        /// Обрабатывает запрос на формирование отчета "Ведомость по счетам".
        /// Рассчитывает начальный баланс, доходы, расходы и конечный баланс
        /// для каждого счета пользователя за указанный период.
        /// </summary>
        /// <param name="viewModel">Модель представления с параметрами отчета.</param>
        /// <returns>
        /// Представление с результатами отчета или ошибками валидации.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountStatement(AccountStatementViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Users = _context.Users
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserId.ToString(),
                        Text = u.Email
                    })
                    .ToList();
                return View(viewModel);
            }

            var user = await _context.Users
                .Include(u => u.Accounts)
                .FirstOrDefaultAsync(u => u.UserId == viewModel.UserId);

            if (user == null)
            {
                ModelState.AddModelError("", "Пользователь не найден");
                viewModel.Users = _context.Users
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserId.ToString(),
                        Text = u.Email
                    })
                    .ToList();
                return View(viewModel);
            }

            viewModel.Email = user.Email;
            viewModel.Accounts = new List<AccountStatementItem>();

            foreach (var account in user.Accounts)
            {
                var transactions = await _context.Transactions
                    .Include(t => t.Category)
                    .Where(t => t.AccountId == account.AccountId &&
                                t.TransactionDate >= viewModel.StartDate &&
                                t.TransactionDate <= viewModel.EndDate)
                    .ToListAsync();

                var previousTransactions = await _context.Transactions
                    .Where(t => t.AccountId == account.AccountId &&
                                t.TransactionDate < viewModel.StartDate)
                    .ToListAsync();

                decimal initialBalance = account.Balance ?? 0;

                foreach (var trans in previousTransactions)
                {
                    var category = await _context.Categories
                        .FirstOrDefaultAsync(c => c.CategoryId == trans.CategoryId);

                    if (category != null)
                    {
                        if (category.CategoryType == "Доход" || category.CategoryType == "Перевод (пополнение)")
                        {
                            initialBalance += trans.Amount ?? 0;
                        }
                        else if (category.CategoryType == "Расход" || category.CategoryType == "Перевод (списание)")
                        {
                            initialBalance -= trans.Amount ?? 0;
                        }
                    }
                }

                decimal totalIncome = 0;
                decimal totalExpense = 0;

                foreach (var trans in transactions)
                {
                    if (trans.Category == null)
                        continue;

                    decimal amount = trans.Amount ?? 0;

                    if (trans.Category.CategoryType == "Доход" ||
                        trans.Category.CategoryType == "Перевод (пополнение)")
                    {
                        totalIncome += amount;
                    }
                    else if (trans.Category.CategoryType == "Расход" ||
                             trans.Category.CategoryType == "Перевод (списание)")
                    {
                        totalExpense += amount;
                    }
                }

                decimal finalBalance = initialBalance + totalIncome - totalExpense;

                viewModel.Accounts.Add(new AccountStatementItem
                {
                    AccountName = account.AccountName,
                    InitialBalance = initialBalance,
                    TotalIncome = totalIncome,
                    TotalExpense = totalExpense,
                    FinalBalance = finalBalance
                });
            }

            viewModel.Users = _context.Users
                .Select(u => new SelectListItem
                {
                    Value = u.UserId.ToString(),
                    Text = u.Email
                })
                .ToList();

            return View(viewModel);
        }

        // GET: Reports/TransactionHistory
        /// <summary>
        /// Отображает форму для формирования отчета "История операций".
        /// Загружает списки счетов и категорий для фильтрации.
        /// </summary>
        /// <returns>Представление с формой для создания отчета.</returns>
        public async Task<IActionResult> TransactionHistory()
        {
            var viewModel = new TransactionHistoryViewModel
            {
                Accounts = await _context.Accounts
                    .Select(a => new SelectListItem
                    {
                        Value = a.AccountId.ToString(),
                        Text = $"{a.AccountName} ({a.User.Email})"
                    })
                    .ToListAsync(),

                Categories = await _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = $"{c.CategoryName} ({c.CategoryType})"
                    })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Reports/TransactionHistory
        /// <summary>
        /// Обрабатывает запрос на формирование отчета по истории транзакций.
        /// Фильтрует транзакции по выбранному счету, категории и периоду,
        /// сортирует по дате и времени в порядке убывания.
        /// </summary>
        /// <param name="viewModel">Модель представления с параметрами фильтрации.</param>
        /// <returns>
        /// Представление с отфильтрованным списком транзакций.
        /// В случае ошибки возвращает форму с сообщением об ошибке.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TransactionHistory(TransactionHistoryViewModel viewModel)
        {
            try
            {
                viewModel.Accounts = await _context.Accounts
                    .Select(a => new SelectListItem
                    {
                        Value = a.AccountId.ToString(),
                        Text = $"{a.AccountName} ({a.User.Email})"
                    })
                    .ToListAsync();

                viewModel.Categories = await _context.Categories
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = $"{c.CategoryName} ({c.CategoryType})"
                    })
                    .ToListAsync();

                var query = _context.Transactions
                    .Include(t => t.Account)
                        .ThenInclude(a => a.User)
                    .Include(t => t.Category)
                    .Where(t => t.TransactionDate >= viewModel.StartDate &&
                                t.TransactionDate <= viewModel.EndDate);

                if (viewModel.AccountId.HasValue)
                {
                    query = query.Where(t => t.AccountId == viewModel.AccountId.Value);
                }

                if (viewModel.CategoryId.HasValue)
                {
                    query = query.Where(t => t.CategoryId == viewModel.CategoryId.Value);
                }

                query = query.OrderByDescending(t => t.TransactionDate)
                             .ThenByDescending(t => t.TransactionTime);

                var transactions = await query.ToListAsync();

                viewModel.Transactions = new List<TransactionHistoryItem>();

                foreach (var transaction in transactions)
                {
                    if (transaction.Account == null || transaction.Category == null)
                        continue;

                    bool isIncome = transaction.Category.CategoryType == "Доход" ||
                                   transaction.Category.CategoryType == "Перевод (пополнение)";

                    var item = new TransactionHistoryItem
                    {
                        TransactionDate = transaction.TransactionDate,
                        TransactionTime = transaction.TransactionTime,
                        AccountName = transaction.Account.AccountName,
                        CategoryName = transaction.Category.CategoryName,
                        OperationType = transaction.Category.CategoryType,
                        Amount = transaction.Amount ?? 0,
                        Description = transaction.Description,
                        IsIncome = isIncome
                    };

                    viewModel.Transactions.Add(item);
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ошибка при формировании отчета: {ex.Message}");
                return View(viewModel);
            }
        }

        // GET: Reports/CategoryAnalysis
        /// <summary>
        /// Отображает форму для формирования анализа расходов/доходов по категориям.
        /// Загружает список пользователей для выбора.
        /// </summary>
        /// <returns>Представление с формой для создания анализа по категориям.</returns>
        public async Task<IActionResult> CategoryAnalysis()
        {
            var viewModel = new CategoryAnalysisViewModel
            {
                Users = await _context.Users
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserId.ToString(),
                        Text = u.Email
                    })
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Reports/CategoryAnalysis
        /// <summary>
        /// Обрабатывает запрос на формирование анализа по категориям.
        /// Группирует транзакции по категориям для выбранного пользователя за указанный период,
        /// рассчитывает общую сумму и количество операций по каждой категории.
        /// </summary>
        /// <param name="viewModel">Модель представления с параметрами анализа.</param>
        /// <returns>
        /// Представление с результатами анализа по категориям.
        /// В случае ошибок возвращает форму с сообщением об ошибке и заполненными списками.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryAnalysis(CategoryAnalysisViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Users = await _context.Users
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserId.ToString(),
                        Text = u.Email
                    })
                    .ToListAsync();
                return View(viewModel);
            }

            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == viewModel.UserId);

                if (user == null)
                {
                    ModelState.AddModelError("", "Пользователь не найден");
                    viewModel.Users = await _context.Users
                        .Select(u => new SelectListItem
                        {
                            Value = u.UserId.ToString(),
                            Text = u.Email
                        })
                        .ToListAsync();
                    return View(viewModel);
                }

                viewModel.Email = user.Email;

                var userAccounts = await _context.Accounts
                    .Where(a => a.UserId == viewModel.UserId)
                    .Select(a => a.AccountId)
                    .ToListAsync();

                var categoryAnalysis = await _context.Transactions
                    .Include(t => t.Category)
                    .Where(t => userAccounts.Contains(t.AccountId.Value) &&
                                t.TransactionDate >= viewModel.StartDate &&
                                t.TransactionDate <= viewModel.EndDate)
                    .GroupBy(t => new
                    {
                        CategoryId = t.CategoryId.Value,
                        CategoryName = t.Category.CategoryName,
                        CategoryType = t.Category.CategoryType
                    })
                    .Select(g => new CategoryAnalysisItem
                    {
                        CategoryName = g.Key.CategoryName,
                        CategoryType = g.Key.CategoryType,
                        TotalAmount = g.Sum(t =>
                            (t.Category.CategoryType == "Доход" || t.Category.CategoryType == "Перевод (пополнение)")
                                ? (t.Amount ?? 0)
                                : -(t.Amount ?? 0)),
                        TransactionCount = g.Count()
                    })
                    .OrderByDescending(c => c.TotalAmount)
                    .ToListAsync();

                viewModel.Categories = categoryAnalysis;

                viewModel.Users = await _context.Users
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserId.ToString(),
                        Text = u.Email
                    })
                    .ToListAsync();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ошибка при формировании отчета: {ex.Message}");
                return View(viewModel);
            }
        }
    }
}