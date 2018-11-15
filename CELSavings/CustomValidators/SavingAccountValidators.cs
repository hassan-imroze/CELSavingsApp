using CELSavings.Models;
using CELSavings.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CELSavings.CustomValidators
{
    public class CodeShouldBeUnique : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is SavingAccount)
            {
                var savingAccount = (SavingAccount)validationContext.ObjectInstance;

                var repository = new SavingAccountRepository();

                bool isAccountNoExists = repository.IsAccountNumberAlreadyExists(savingAccount.AccountNo, savingAccount.Id);

                repository.Dispose();

                return isAccountNoExists ? new ValidationResult("Account Number already exists") : ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Custom validation attribute used for wrong object");
            }
        }
    }

    public class NameShouldBeUnique : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is SavingAccount)
            {
                var savingAccount = (SavingAccount)validationContext.ObjectInstance;

                var repository = new SavingAccountRepository();

                bool isNmaeExists = repository.IsNameAlreadyExists(savingAccount.Name, savingAccount.Id);
                
                repository.Dispose();

                return isNmaeExists ? new ValidationResult("Account Name already exists") : ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Custom validation attribute used for wrong object");
            }
        }
    }
}