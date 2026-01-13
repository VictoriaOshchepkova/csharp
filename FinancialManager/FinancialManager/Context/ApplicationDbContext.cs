using FinancialManager.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialManager.Context
{
    /// <summary>
    /// Контекст базы данных приложения для управления финансами.
    /// Определяет модель данных и конфигурацию сущностей для Entity Framework Core.
    /// </summary>
    public class ApplicationDbContext: DbContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр контекста базы данных с указанными параметрами.
        /// </summary>
        /// <param name="options">Параметры конфигурации контекста базы данных.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Набор данных для работы с пользователями системы.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Набор данных для работы со счетами пользователей.
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Набор данных для работы с категориями транзакций.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Набор данных для работы с финансовыми транзакциями.
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Набор данных для работы с бюджетами пользователей.
        /// </summary>
        public DbSet<Budget> Budgets { get; set; }

        /// <summary>
        /// Конфигурирует модель данных при создании контекста.
        /// Определяет связи, ограничения, индексы и другие параметры сущностей.
        /// </summary>
        /// <param name="modelBuilder">Строитель модели для настройки сущностей.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(20).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Patronymic).HasMaxLength(20);
                entity.Property(e => e.Password).HasMaxLength(255).IsRequired();
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountId);
                entity.HasIndex(e => new { e.UserId, e.AccountName }).IsUnique();
                entity.Property(e => e.AccountName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.AccountType).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Balance).HasPrecision(15, 2);
                entity.Property(e => e.CreditLimit).HasPrecision(15, 2);

                entity.HasOne(a => a.User)
                      .WithMany(u => u.Accounts)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.HasIndex(e => e.CategoryName).IsUnique();
                entity.Property(e => e.CategoryName).HasMaxLength(100).IsRequired();
                entity.Property(e => e.CategoryType)
                      .HasMaxLength(50)
                      .IsRequired()
                      .HasConversion(
                          v => v.ToString(),
                          v => v);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId); 

                entity.HasIndex(e => new { e.AccountId, e.TransactionDate, e.TransactionTime })
                      .IsUnique();

                entity.Property(e => e.Amount).HasPrecision(15, 2).IsRequired();
                entity.Property(e => e.Description);

                entity.HasOne(t => t.Account)
                      .WithMany(a => a.Transactions)
                      .HasForeignKey(t => t.AccountId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.Category)
                      .WithMany(c => c.Transactions)
                      .HasForeignKey(t => t.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.HasKey(e => e.BudgetId);

                entity.HasIndex(e => new { e.UserId, e.CategoryId, e.StartDate })
                      .IsUnique();

                entity.Property(e => e.PlannedAmount).HasPrecision(15, 2).IsRequired();

                entity.HasOne(b => b.User)
                      .WithMany(u => u.Budgets)
                      .HasForeignKey(b => b.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Category)
                      .WithMany(c => c.Budgets)
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}