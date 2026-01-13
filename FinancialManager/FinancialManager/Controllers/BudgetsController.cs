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
    /// Контроллер для управления бюджетами пользователей.
    /// Обеспечивает CRUD операции для сущности Budget.
    /// </summary>
    public class BudgetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера, принимающий контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных для работы с сущностями.</param>
        public BudgetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Budgets
        /// <summary>
        /// Отображает список всех бюджетов с включенными связанными данными (категория и пользователь).
        /// </summary>
        /// <returns>Представление со списком бюджетов.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Budgets.Include(b => b.Category).Include(b => b.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Budgets/Details/5
        /// <summary>
        /// Отображает детальную информацию о конкретном бюджете.
        /// Находит бюджет по идентификатору и проверяет его существование.
        /// </summary>
        /// <param name="id">Идентификатор бюджета (BudgetId).</param>
        /// <returns>
        /// Представление с деталями бюджета, если найден. 
        /// NotFound, если бюджет не найден или id не указан.
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgets
                .Include(b => b.Category)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BudgetId == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // GET: Budgets/Create
        /// <summary>
        /// Отображает форму для создания нового бюджета.
        /// Загружает списки категорий и пользователей для выбора в форме.
        /// </summary>
        /// <returns>Представление с формой создания бюджета.</returns>
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Budgets/Create
        /// <summary>
        /// Обрабатывает отправку формы создания нового бюджета.
        /// Проверяет уникальность бюджета для комбинации пользователь-категория-дата начала,
        /// валидирует модель и сохраняет бюджет в БД.
        /// </summary>
        /// <param name="budget">Объект бюджета, заполненный данными из формы.</param>
        /// <returns>
        /// Перенаправление на список бюджетов при успешном создании.
        /// Представление с формой и ошибками валидации при неудаче.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BudgetId,UserId,CategoryId,StartDate,PlannedAmount,CurrencyBudget,EndDate")] Budget budget)
        {
            var existingBudget = await _context.Budgets
               .FirstOrDefaultAsync(b => b.UserId == budget.UserId
                   && b.CategoryId == budget.CategoryId
                   && b.StartDate == budget.StartDate);

            if (existingBudget != null)
            {
                ModelState.AddModelError("", "Бюджет для этого пользователя, категории и начальной даты уже существует");
            }

            if (ModelState.IsValid)
            {
                _context.Add(budget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", budget.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", budget.UserId);
            return View(budget);
        }

        // GET: Budgets/Edit/5
        /// <summary>
        /// Отображает форму для редактирования существующего бюджета.
        /// Находит бюджет по id и проверяет его существование.
        /// </summary>
        /// <param name="id">Идентификатор редактируемого бюджета.</param>
        /// <returns>
        /// Представление с формой редактирования, если бюджет найден.
        /// NotFound, если бюджет не найден или id не указан.
        /// </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", budget.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", budget.UserId);
            return View(budget);
        }

        // POST: Budgets/Edit/5
        /// <summary>
        /// Обрабатывает отправку формы редактирования бюджета.
        /// Проверяет уникальность бюджета для комбинации пользователь-категория-дата начала (кроме текущего бюджета),
        /// валидирует модель и обновляет данные.
        /// </summary>
        /// <param name="id">Идентификатор бюджета.</param>
        /// <param name="budget">Объект бюджета с обновленными данными.</param>
        /// <returns>
        /// Перенаправление на список бюджетов при успешном обновлении.
        /// Представление с формой и ошибками при неудаче.
        /// NotFound при несоответствии id или отсутствии бюджета.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BudgetId,UserId,CategoryId,StartDate,PlannedAmount,CurrencyBudget,EndDate")] Budget budget)
        {
            if (id != budget.BudgetId)
            {
                return NotFound();
            }

            var existingBudget = await _context.Budgets
                .FirstOrDefaultAsync(b => b.UserId == budget.UserId
                    && b.CategoryId == budget.CategoryId
                    && b.StartDate == budget.StartDate
                    && b.BudgetId != budget.BudgetId);

            if (existingBudget != null)
            {
                ModelState.AddModelError("", "Бюджет для этого пользователя, категории и начальной даты уже существует");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(budget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudgetExists(budget.BudgetId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", budget.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", budget.UserId);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        /// <summary>
        /// Отображает форму подтверждения удаления бюджета.
        /// Находит бюджет по id и показывает его данные перед удалением.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого бюджета.</param>
        /// <returns>
        /// Представление с подтверждением удаления, если бюджет найден.
        /// NotFound, если бюджет не найден или id не указан.
        /// </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = await _context.Budgets
                .Include(b => b.Category)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.BudgetId == id);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: Budgets/Delete/5
        /// <summary>
        /// Выполняет удаление бюджета после подтверждения.
        /// Находит бюджет по id, удаляет его из БД и сохраняет изменения.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого бюджета.</param>
        /// <returns>Перенаправление на список бюджетов.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget != null)
            {
                _context.Budgets.Remove(budget);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование бюджета по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор бюджета для проверки.</param>
        /// <returns>true, если бюджет существует; false, если нет.</returns>
        private bool BudgetExists(int id)
        {
            return _context.Budgets.Any(e => e.BudgetId == id);
        }
    }
}
