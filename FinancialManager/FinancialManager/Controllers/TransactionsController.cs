using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancialManager.Context;
using FinancialManager.Models;

namespace FinancialManager.Controllers
{
    /// <summary>
    /// Контроллер для управления финансовыми транзакциями.
    /// Обеспечивает CRUD операции для сущности Transaction.
    /// </summary>
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера, принимающий контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных для работы с сущностями.</param>
        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        /// <summary>
        /// Отображает список всех транзакций в системе.
        /// </summary>
        /// <returns>Представление с коллекцией транзакций и связанными данными.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.Account).Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        /// <summary>
        /// Отображает детальную информацию о конкретной транзакции.
        /// </summary>
        /// <param name="id">Идентификатор транзакции (TransactionId).</param>
        /// <returns>
        /// Представление с деталями транзакции, если найдена.
        /// NotFound (404), если транзакция не найдена или id не указан.
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        /// <summary>
        /// Отображает форму для создания новой транзакции.
        /// Подготавливает данные для выпадающих списков (счета, категории).
        /// </summary>
        /// <returns>Представление с формой создания транзакции.</returns>
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Transactions/Create
        /// <summary>
        /// Обрабатывает отправку формы создания новой транзакции.
        /// Проверяет уникальность транзакции по дате, времени и счету, валидирует модель и сохраняет в БД.
        /// </summary>
        /// <param name="transaction">Объект транзакции, заполненный данными из формы.</param>
        /// <returns>
        /// Перенаправление на список транзакций при успешном создании.
        /// Представление с формой и ошибками валидации при неудаче.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId,AccountId,TransactionDate,TransactionTime,CategoryId,Amount,Description")] Transaction transaction)
        {
            var existingTransaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.AccountId == transaction.AccountId
                    && t.TransactionDate == transaction.TransactionDate
                    && t.TransactionTime == transaction.TransactionTime);

            if (existingTransaction != null)
            {
                ModelState.AddModelError("", "Транзакция на этом счету с такой датой и временем уже существует");
            }

            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountName", transaction.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", transaction.CategoryId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        /// <summary>
        /// Отображает форму для редактирования существующей транзакции.
        /// Находит транзакцию по id и подготавливает выпадающие списки с текущими значениями.
        /// </summary>
        /// <param name="id">Идентификатор редактируемой транзакции.</param>
        /// <returns>
        /// Представление с формой редактирования, если транзакция найдена.
        /// NotFound, если транзакция не найдена или id не указан.
        /// </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountName", transaction.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", transaction.CategoryId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        /// <summary>
        /// Обрабатывает отправку формы редактирования транзакции.
        /// Проверяет уникальность транзакции (исключая текущую), валидирует модель и обновляет данные.
        /// </summary>
        /// <param name="id">Идентификатор транзакции из маршрута.</param>
        /// <param name="transaction">Объект транзакции с обновленными данными.</param>
        /// <returns>
        /// Перенаправление на список транзакций при успешном обновлении.
        /// Представление с формой и ошибками при неудаче.
        /// NotFound при несоответствии id или отсутствии транзакции.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionId,AccountId,TransactionDate,TransactionTime,CategoryId,Amount,Description")] Transaction transaction)
        {
            if (id != transaction.TransactionId)
            {
                return NotFound();
            }

            var existingTransaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.AccountId == transaction.AccountId
                    && t.TransactionDate == transaction.TransactionDate
                    && t.TransactionTime == transaction.TransactionTime
                    && t.TransactionId != transaction.TransactionId);

            if (existingTransaction != null)
            {
                ModelState.AddModelError("", "Транзакция на этом счету с такой датой и временем уже существует");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.TransactionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountName", transaction.AccountId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", transaction.CategoryId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        /// <summary>
        /// Отображает форму подтверждения удаления транзакции.
        /// Показывает данные транзакции и информацию о счете и категории перед удалением.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой транзакции.</param>
        /// <returns>
        /// Представление с подтверждением удаления, если транзакция найдена.
        /// NotFound, если транзакция не найдена или id не указан.
        /// </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.TransactionId == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        /// <summary>
        /// Выполняет удаление транзакции после подтверждения.
        /// Удаляет транзакцию.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой транзакции.</param>
        /// <returns>Перенаправление на список транзакций.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование транзакции по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор транзакции для проверки.</param>
        /// <returns>true, если транзакция существует; false, если нет.</returns>
        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }
    }
}
