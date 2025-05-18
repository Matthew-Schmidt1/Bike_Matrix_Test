using System.ComponentModel.DataAnnotations;

namespace BikeMatrixModels.Attribute
{
    public class PreventSqlInjectionAttribute : ValidationAttribute
    {
        public PreventSqlInjectionAttribute()
        {
            ErrorMessage = "Potential SQL injection detected";
        }
        private readonly string[] SqlKeywords =
        {
        "SELECT", "INSERT", "UPDATE", "DELETE", "DROP", "ALTER", "CREATE", "EXEC", "--", ";", "'"
         };

        public override bool IsValid(object value)
        {
            if (value is string input)
            {
                // Convert to uppercase for case-insensitive matching
                string upperInput = input.ToUpperInvariant();

                // Check for SQL keywords
                foreach (var keyword in SqlKeywords)
                {
                    if (upperInput.Contains(keyword))
                    {
                        return false; // SQL injection detected
                    }
                }

                return true; // Input is safe
            }

            return false; // Invalid type
        }
    }
}
