using CELSavings.Models;
using CELSavings.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CELSavings.CustomValidators
{
    
    public class EmailDoesNotExistsInSavingsAccount : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is RegisterViewModel)
            {
                var registerVModel = (RegisterViewModel)validationContext.ObjectInstance;

                var repository = new SavingAccountRepository();

                bool isEmailExists = repository.EmailExists(registerVModel.Email);

                repository.Dispose();

                return isEmailExists ? ValidationResult.Success : new ValidationResult("Could not find savings account with this email address");
            }
            else
            {
                return new ValidationResult("Custom validation attribute used for wrong object");
            }
        }
    }
}