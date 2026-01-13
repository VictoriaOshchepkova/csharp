using FinancialManager.Context;
using FinancialManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace FinancialManager.Controllers
{
    /// <summary>
    /// Контроллер для управления категориями финансовых операций.
    /// Обеспечивает CRUD операции для сущности Category.
    /// </summary>
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера, принимающий контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных для работы с сущностями.</param>
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список доступных типов категорий для выпадающего списка.
        /// Определяет возможные типы финансовых операций в системе.
        /// </summary>
        /// <param name="selectedValue">Предварительно выбранное значение.</param>
        /// <returns>Список SelectListItem с типами категорий.</returns>
        private List<SelectListItem> GetCategoryTypes(string selectedValue = null)
        {
            var categoryTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = "Доход", Text = "Доход", Selected = selectedValue == "Доход" },
                new SelectListItem { Value = "Расход", Text = "Расход", Selected = selectedValue == "Расход" },
                new SelectListItem { Value = "Перевод (списание)", Text = "Перевод (списание)", Selected = selectedValue == "Перевод (списание)" },
                new SelectListItem { Value = "Перевод (пополнение)", Text = "Перевод (пополнение)", Selected = selectedValue == "Перевод (пополнение)" }
            };

            return categoryTypes;
        }

        // GET: Categories
        /// <summary>
        /// Отображает список всех категорий в системе.
        /// </summary>
        /// <returns>Представление с коллекцией всех категорий.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        /// <summary>
        /// Отображает детальную информацию о конкретной категории.
        /// </summary>
        /// <param name="id">Идентификатор категории (CategoryId).</param>
        /// <returns>
        /// Представление с деталями категории, если найдена.
        /// NotFound, если категория не найдена или id не указан.
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        /// <summary>
        /// Отображает форму для создания новой категории.
        /// Подготавливает данные для выпадающего списка типов категорий.
        /// </summary>
        /// <returns>Представление с формой создания категории.</returns>
        public IActionResult Create()
        {
            ViewData["CategoryType"] = new SelectList(GetCategoryTypes(), "Value", "Text");

            return View();
        }

        // POST: Categories/Create
        /// <summary>
        /// Обрабатывает отправку формы создания новой категории.
        /// Проверяет уникальность названия категории, валидирует модель и сохраняет в БД.
        /// </summary>
        /// <param name="category">Объект категории, заполненный данными из формы.</param>
        /// <returns>
        /// Перенаправление на список категорий при успешном создании.
        /// Представление с формой и ошибками валидации при неудаче.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryType")] Category category)
        {
            var existingCategory = await _context.Categories
               .FirstOrDefaultAsync(c => c.CategoryName == category.CategoryName);

            if (existingCategory != null)
            {
                ModelState.AddModelError("CategoryName",
                    $"Категория с названием '{category.CategoryName}' уже существует");
            }

            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryType"] = new SelectList(GetCategoryTypes(), "Value", "Text");

            return View(category);
        }

        // GET: Categories/Edit/5
        /// <summary>
        /// Отображает форму для редактирования существующей категории.
        /// Находит категорию по id и подготавливает выпадающий список с текущим типом.
        /// </summary>
        /// <param name="id">Идентификатор редактируемой категории.</param>
        /// <returns>
        /// Представление с формой редактирования, если категория найдена.
        /// NotFound, если категория не найдена или id не указан.
        /// </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            ViewData["CategoryType"] = new SelectList(GetCategoryTypes(), "Value", "Text");

            return View(category);
        }

        // POST: Categories/Edit/5
        /// <summary>
        /// Обрабатывает отправку формы редактирования категории.
        /// Проверяет уникальность названия категории (исключая текущую), валидирует модель и обновляет данные.
        /// </summary>
        /// <param name="id">Идентификатор категории из маршрута.</param>
        /// <param name="category">Объект категории с обновленными данными.</param>
        /// <returns>
        /// Перенаправление на список категорий при успешном обновлении.
        /// Представление с формой и ошибками при неудаче.
        /// NotFound при несоответствии id или отсутствии категории
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,CategoryType")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryName == category.CategoryName && c.CategoryId != category.CategoryId);

            if (existingCategory != null)
            {
                ModelState.AddModelError("CategoryName",
                    $"Категория с названием '{category.CategoryName}' уже существует");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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

            ViewData["CategoryType"] = new SelectList(GetCategoryTypes(), "Value", "Text");

            return View(category);
        }

        // GET: Categories/Delete/5
        /// <summary>
        /// Отображает форму подтверждения удаления категории.
        /// Проверяет наличие связанных транзакций и бюджетов перед удалением.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой категории.</param>
        /// <returns>
        /// Представление с подтверждением удаления, если категория не используется.
        /// Перенаправление на список с сообщением об ошибке, если категория используется.
        /// NotFound, если категория не найдена или id не указан.
        /// </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Transactions)
                .Include(c => c.Budgets)
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            if (category.Transactions.Any() || category.Budgets.Any())
            {
                TempData["ErrorMessage"] = $"Нельзя удалить категорию '{category.CategoryName}', так как она используется в транзакциях или бюджетах.";
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        /// <summary>
        /// Выполняет удаление категории после подтверждения.
        /// Удаляет категорию только если она не используется.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой категории.</param>
        /// <returns>Перенаправление на список категорий.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// Проверяет существование категории по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор категории для проверки.</param>
        /// <returns>true, если категория существует; false, если нет.</returns>
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
