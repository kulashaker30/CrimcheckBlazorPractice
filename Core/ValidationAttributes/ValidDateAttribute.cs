using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineClinic.Core.ValidationAttributes
{
    public class ValidDateAttribute : ValidationAttribute
    {
        private string Name;

        public ValidDateAttribute(string name)
        {
            Name = name;
        }

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var valueString = value.ToString();
            DateTime date;
            if(!DateTime.TryParse(valueString, out date))
                return new ValidationResult($"Invalid Date for {Name} field.");

            return ValidationResult.Success;
        }
    }
}