namespace ConsoleApp2_1
{
    /// <summary>
    /// Базовый абстрактный класс для сущностей имени.
    /// Предоставляет общую логику валидации компонентов имени.
    /// </summary>
    public abstract class NameBase // DRY
    {
        /// <summary>
        /// Проверяет, что значение начинается с заглавной буквы (если оно не null и не пустое).
        /// </summary>
        /// <param name="value">Проверяемое значение.</param>
        /// <param name="fieldName">Название поля для сообщения об ошибке.</param>
        /// <exception cref="ArgumentException">Выбрасывается, если значение начинается не с заглавной буквы.</exception>
        protected static void ValidateCapitalized(string? value, string fieldName)
        {
            if (!string.IsNullOrEmpty(value) && !char.IsUpper(value[0]))
            {
                throw new ArgumentException($"Error: The {fieldName} must begin with a capital letter.");
            }
        }
    }
}