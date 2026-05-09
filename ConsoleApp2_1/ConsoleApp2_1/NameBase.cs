namespace ConsoleApp2_1
{
    public abstract class NameBase // DRY
    {
        protected static void ValidateCapitalized(string? value, string fieldName)
        {
            if (!string.IsNullOrEmpty(value) && !char.IsUpper(value[0]))
            {
                throw new ArgumentException($"Error: The {fieldName} must begin with a capital letter.");
            }
        }
    }
}
