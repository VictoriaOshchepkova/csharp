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
    /// Контроллер для управления финансовыми счетами пользователей.
    /// Обеспечивает CRUD операции для сущности Account.
    /// </summary>
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера, принимающий контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных для работы с сущностями.</param>
        public AccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список доступных типов счетов для выпадающего списка.
        /// Используется для заполнения ViewData в методах Create и Edit.
        /// </summary>
        /// <param name="selectedValue">Предварительно выбранное значение.</param>
        /// <returns>Список SelectListItem с типами счетов.</returns>
        private List<SelectListItem> GetAccountTypes(string selectedValue = null)
        {
            var accountTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Дебетовый", Text = "Дебетовый" },
                new SelectListItem { Value = "Накопительный", Text = "Накопительный" },
                new SelectListItem { Value = "Наличный", Text = "Наличный" },
                new SelectListItem { Value = "Кредитный", Text = "Кредитный" }
            };

            return accountTypes;
        }

        // GET: Accounts
        /// <summary>
        /// Отображает список всех счетов в системе.
        /// </summary>
        /// <returns>Представление с коллекцией счетов и связанными пользователями.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Accounts.Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Accounts/Details/5
        /// <summary>
        /// Отображает детальную информацию о конкретном счете.
        /// </summary>
        /// <param name="id">Идентификатор счета (AccountId).</param>
        /// <returns>
        /// Представление с деталями счета, если найден. 
        /// NotFound, если счет не найден или id не указан.
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        /// <summary>
        /// Отображает форму для создания нового счета.
        /// Подготавливает данные для выпадающих списков (типы счетов, пользователи).
        /// </summary>
        /// <returns>Представление с формой создания счета.</returns>
        public IActionResult Create()
        {
            ViewData["AccountType"] = new SelectList(GetAccountTypes(), "Value", "Text");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Accounts/Create
        /// <summary>
        /// Обрабатывает отправку формы создания нового счета.
        /// Проверяет уникальность названия счета для данного пользователя, валидирует модель и сохраняет в БД.
        /// </summary>
        /// <param name="account">Объект счета, заполненный данными из формы.</param>
        /// <returns>
        /// Перенаправление на список счетов при успешном создании.
        /// Представление с формой и ошибками валидации при неудаче.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,UserId,AccountName,AccountType,Balance,Currency,CreditLimit")] Account account)
        {
            var existingAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.UserId == account.UserId && a.AccountName == account.AccountName);

            if (existingAccount != null)
            {
                ModelState.AddModelError("AccountName",
                    $"У пользователя уже есть счет с названием '{account.AccountName}'");
            }

            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AccountType"] = new SelectList(GetAccountTypes(), "Value", "Text");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", account.UserId);
            return View(account);
        }

        // GET: Accounts/Edit/5
        /// <summary>
        /// Отображает форму для редактирования существующего счета.
        /// Находит счет по id и подготавливает выпадающие списки с текущими значениями.
        /// </summary>
        /// <param name="id">Идентификатор редактируемого счета.</param>
        /// <returns>
        /// Представление с формой редактирования, если счет найден.
        /// NotFound, если счет не найден или id не указан.
        /// </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            ViewData["AccountType"] = new SelectList(GetAccountTypes(), "Value", "Text");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", account.UserId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        /// <summary>
        /// Обрабатывает отправку формы редактирования счета.
        /// Проверяет уникальность названия счета (исключая текущий счет), валидирует модель и обновляет данные.
        /// </summary>
        /// <param name="id">Идентификатор счета.</param>
        /// <param name="account">Объект счета с обновленными данными.</param>
        /// <returns>
        /// Перенаправление на список счетов при успешном обновлении.
        /// Представление с формой и ошибками при неудаче.
        /// NotFound при несоответствии id или отсутствии счета.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,UserId,AccountName,AccountType,Balance,Currency,CreditLimit")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            var existingAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.UserId == account.UserId
                               && a.AccountName == account.AccountName
                               && a.AccountId != account.AccountId);

            if (existingAccount != null)
            {
                ModelState.AddModelError("AccountName",
                    $"У пользователя уже есть счет с названием '{account.AccountName}'");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
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

            ViewData["AccountType"] = new SelectList(GetAccountTypes(), "Value", "Text");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", account.UserId);
            return View(account);
        }

        // GET: Accounts/Delete/5
        /// <summary>
        /// Отображает форму подтверждения удаления счета.
        /// Показывает данные счета и информацию о владельце перед удалением.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого счета.</param>
        /// <returns>
        /// Представление с подтверждением удаления, если счет найден.
        /// NotFound, если счет не найден или id не указан.
        /// </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        /// <summary>
        /// Выполняет удаление счета после подтверждения.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого счета.</param>
        /// <returns>Перенаправление на список счетов.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование счета по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор счета для проверки.</param>
        /// <returns>true, если счет существует; false, если нет.</returns>
        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
