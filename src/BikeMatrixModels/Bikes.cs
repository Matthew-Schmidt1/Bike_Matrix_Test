using BikeMatrixModels.Attribute;
using System.ComponentModel.DataAnnotations;

namespace BikeMatrixModels
{
    public class Bikes
    {
        public int id { get; set; }
        [Required]
        [EmailAddress]
        [PreventSqlInjection]
        public string EmailAddress { get; set; }
        [Required]
        [PreventSqlInjection]
        public string Brand { get; set; }
        [Required]
        [PreventSqlInjection]
        public string Model { get; set; }
        [Required]
        [Range(1800, int.MaxValue, ErrorMessage = "YearOfManufactor must be at least 1800.")]
        public int YearOfManufactor { get; set; }


        public bool ValidateObject( out List<string> errors)
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, context, results, true);

            errors = results.ConvertAll(r => r.ErrorMessage);
            return isValid;
        }
    }
}
