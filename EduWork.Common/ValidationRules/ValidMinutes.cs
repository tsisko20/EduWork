using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWork.Common.ValidationRules
{
    public class ValidMinutesAttribute : ValidationAttribute, IValidatableObject
    {
        private readonly int[] ValidMinutes = { 0, 15, 30, 45 };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is TimeOnly time)
            {
                if (!ValidMinutes.Contains(time.Minute))
                {
                    return new ValidationResult($"Minute moraju imati vrijednost 0, 15, 30 ili 45.");
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Array.Empty<ValidationResult>();
        }
    }
}
