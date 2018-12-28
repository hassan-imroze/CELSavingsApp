using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CELSavings.CustomValidators
{
    public class DoubleLessThanEqual : ValidationAttribute
    {
        string _propertyNameToCheck;
        string _propertyNameToCheckDisplay;

        public DoubleLessThanEqual(string PropertyNameToCheck)
        {
            _propertyNameToCheck = PropertyNameToCheck;
            _propertyNameToCheckDisplay = PropertyNameToCheck;
        }

        public DoubleLessThanEqual(string PropertyNameToCheck, string PropertyNametoCheckDispaly)
        {
            _propertyNameToCheck = PropertyNameToCheck;
            _propertyNameToCheckDisplay = PropertyNametoCheckDispaly;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double doubleValue;

            if (!double.TryParse(value.ToString(), out doubleValue))
            {
                throw
                    new ArgumentException("The Attribute only validates Double Types.");
            }

            var propertyName = validationContext.ObjectType.GetProperty(_propertyNameToCheck);
            if (propertyName == null)
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", new[] { _propertyNameToCheck }));

            var propertyValue = Convert.ToDouble(propertyName.GetValue(validationContext.ObjectInstance, null));


            return doubleValue <= propertyValue ?
                   ValidationResult.Success :
                   new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, _propertyNameToCheckDisplay)); 
        }
    }
}