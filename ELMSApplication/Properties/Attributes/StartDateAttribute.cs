

using System;
using System.ComponentModel.DataAnnotations;

namespace ELMSApplication.Properties.Attributes
{
    public class StartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime startDate = (DateTime)value;
            DateTime endDate = Convert.ToDateTime(validationContext.ObjectType.GetProperty("EndDate")?.GetValue(validationContext.ObjectInstance));

            if (startDate >= endDate)
            {
                return new ValidationResult("Start date must be a previous date than the end date.");
            }

            return ValidationResult.Success;
        }
    }
}

