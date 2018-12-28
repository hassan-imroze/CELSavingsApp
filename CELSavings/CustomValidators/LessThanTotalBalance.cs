using CELSavings.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CELSavings.CustomValidators
{
    public class LessThanTotalBalance : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var repository = new SavingAccountRepository();

            var totalBalance = repository.GetSavingsAccounts().Where(x=> x.Status == MemberStatus.Live).Sum(x => x.Balance);

            repository.Dispose();

            decimal decimalValue = 0;
            
            if (value != null)
            {
                if (!Decimal.TryParse(value.ToString(), out decimalValue))
                {
                    throw
                        new ArgumentException("The Attribute only validates Decimal Types.");
                }
            }
            

            return decimalValue <= totalBalance ? ValidationResult.Success : new ValidationResult(string.Format("{0} is greater than total fund. Total Fund is : {1} BDT", validationContext.DisplayName,totalBalance));

        }
    }
}