using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CELSavings.CustomValidators
{
    public class DoubleGreaterThan : ValidationAttribute
    {

        string _propertyNameToCheck;
        string _propertyNameToCheckDisplay;

        public DoubleGreaterThan(string PropertyNameToCheck)
        {
            _propertyNameToCheck = PropertyNameToCheck;
            _propertyNameToCheckDisplay = PropertyNameToCheck;
        }

        public DoubleGreaterThan(string PropertyNameToCheck,string PropertyNametoCheckDispaly)
        {
            _propertyNameToCheck = PropertyNameToCheck;
            _propertyNameToCheckDisplay = PropertyNametoCheckDispaly;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            double decimalValue;

            if (!double.TryParse(value.ToString(), out decimalValue))
            {
                throw 
                    new ArgumentException("The Attribute only validates Double Types.");
            }

            var propertyName = validationContext.ObjectType.GetProperty(_propertyNameToCheck);
            if (propertyName == null)
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", new[] { _propertyNameToCheck }));

            var propertyValue = Convert.ToDouble(propertyName.GetValue(validationContext.ObjectInstance, null));


            return decimalValue > propertyValue ? 
                   ValidationResult.Success :
                   new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, _propertyNameToCheckDisplay)); ;
        }
    }

    public class DateGreaterThanOrEqual : ValidationAttribute
    {

        string _propertyNameToCheck;
        string _propertyNameToCheckDisplay;

        public DateGreaterThanOrEqual(string PropertyNameToCheck)
        {
            _propertyNameToCheck = PropertyNameToCheck;
            _propertyNameToCheckDisplay = PropertyNameToCheck;
        }

        public DateGreaterThanOrEqual(string PropertyNameToCheck, string PropertyNametoCheckDispaly)
        {
            _propertyNameToCheck = PropertyNameToCheck;
            _propertyNameToCheckDisplay = PropertyNametoCheckDispaly;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateValue=DateTime.MinValue;

            if (value != null)
            {
                if (!DateTime.TryParse(value.ToString(), out dateValue))
                {
                    throw
                        new ArgumentException("The Attribute only validates Date Types.");
                }
            }

            var propertyName = validationContext.ObjectType.GetProperty(_propertyNameToCheck);
            if (propertyName == null)
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "Unknown property {0}", new[] { _propertyNameToCheck }));

            var propertyValue = propertyName.GetValue(validationContext.ObjectInstance, null);
            var propertyDateValue = DateTime.MinValue;

            if (propertyValue !=null)
            {
                if (!DateTime.TryParse(propertyValue.ToString(), out propertyDateValue))
                {
                    throw
                        new ArgumentException("The Property to check is not Date Type.");
                }
            }
            


            return dateValue >= propertyDateValue ?
                   ValidationResult.Success :
                   new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName, _propertyNameToCheckDisplay)); ;
        }
    }
}