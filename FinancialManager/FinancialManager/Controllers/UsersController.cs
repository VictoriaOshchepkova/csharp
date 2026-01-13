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
    /// Контроллер для управления пользователями.
    /// Обеспечивает CRUD операции для сущности User.
    /// </summary>
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Конструктор контроллера, принимающий контекст базы данных.
        /// </summary>
        /// <param name="context">Контекст базы данных для работы с сущностями.</param>
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        /// <summary>
        /// Отображает список всех пользователей системы.
        /// </summary>
        /// <returns>Представление с коллекцией пользователей.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        /// <summary>
        /// Отображает детальную информацию о конкретном пользователе.
        /// Находит пользователя по идентификатору и проверяет его существование.
        /// </summary>
        /// <param name="id">Идентификатор пользователя (UserId).</param>
        /// <returns>
        /// Представление с деталями пользователя, если найден. 
        /// NotFound, если пользователь не найден или id не указан.
        /// </returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        /// <summary>
        /// Отображает форму для создания нового пользователя.
        /// </summary>
        /// <returns>Представление с формой создания пользователя.</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        /// <summary>
        /// Обрабатывает отправку формы создания нового пользователя.
        /// Проверяет уникальность email, валидирует модель и сохраняет пользователя в БД.
        /// </summary>
        /// <param name="user">Объект пользователя, заполненный данными из формы.</param>
        /// <returns>
        /// Перенаправление на список пользователей при успешном создании.
        /// Представление с формой и ошибками валидации при неудаче.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Email,LastName,FirstName,Patronymic,Password")] User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Пользователь с таким email уже существует");
            }

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        /// <summary>
        /// Отображает форму для редактирования существующего пользователя.
        /// Находит пользователя по id и проверяет его существование.
        /// </summary>
        /// <param name="id">Идентификатор редактируемого пользователя.</param>
        /// <returns>
        /// Представление с формой редактирования, если пользователь найден.
        /// NotFound, если пользователь не найден или id не указан.
        /// </returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        /// <summary>
        /// Обрабатывает отправку формы редактирования пользователя.
        /// Проверяет уникальность email (кроме текущего пользователя), валидирует модель и обновляет данные.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <param name="user">Объект пользователя с обновленными данными.</param>
        /// <returns>
        /// Перенаправление на список пользователей при успешном обновлении.
        /// Представление с формой и ошибками при неудаче.
        /// NotFound при несоответствии id или отсутствии пользователя.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Email,LastName,FirstName,Patronymic,Password")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email && u.UserId != user.UserId);

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Пользователь с таким email уже существует");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        /// <summary>
        /// Отображает форму подтверждения удаления пользователя.
        /// Находит пользователя по id и показывает его данные перед удалением.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого пользователя.</param>
        /// <returns>
        /// Представление с подтверждением удаления, если пользователь найден.
        /// NotFound, если пользователь не найден или id не указан.
        /// </returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        /// <summary>
        /// Выполняет удаление пользователя после подтверждения.
        /// Находит пользователя по id, удаляет его из БД и сохраняет изменения.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого пользователя.</param>
        /// <returns>Перенаправление на список пользователей.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Проверяет существование пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя для проверки.</param>
        /// <returns>true, если пользователь существует; false, если нет.</returns>
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
