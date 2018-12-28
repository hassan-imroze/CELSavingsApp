using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CELSavings.CustomValidators
{
    public class DateRequired : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateValue;

            if (value != null)
            {
                if (!DateTime.TryParse(value.ToString(), out dateValue))
                {
                    throw
                        new ArgumentException("The Attribute only validates DateTime Types.");
                }
            }
            else
            {
                dateValue = DateTime.MinValue;
            }

          

            return dateValue!= DateTime.MinValue ?
                   ValidationResult.Success :
                   new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName)); ;
        }
    }
}