using System.ComponentModel.DataAnnotations;

namespace ContactAPI.model
{
    public class Contact
    {
        [Required(ErrorMessage = "Id is Mandatory")]
        [GreaterThanZero]
        public int Id { get; set; }
        [Required (ErrorMessage ="Name is Mandatory")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is Mandatory")]
        [EmailAddress(ErrorMessage = "Sorry, this doesn't look like a valid email address.  Please use the format xxx@xxx.xx")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "City is Mandatory")]
        public string? City { get; set; }
    }

    public class GreaterThanZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is int intValue && intValue > 0)
            {
                return ValidationResult.Success!;
            }
            return new ValidationResult("The field must be greater than 0.");
        }
    }
}