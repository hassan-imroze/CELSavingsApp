using CELSavings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.ViewModels
{
    public class AccountStatementViewModel
    {
        public string AccountNo { get; set; }

        public string AccountName { get; set; }

        public string Email { get; set; }
        
        public string Mobile { get; set; }

        public string NID { get; set; }


        public static AccountStatementViewModel CreateObject(SavingAccount savingAccount)
        {
            AccountStatementViewModel accountStatementViewModel = new AccountStatementViewModel
            {
                AccountNo = savingAccount.AccountNo,
                AccountName = savingAccount.Name,
                Email = string.IsNullOrWhiteSpace(savingAccount.Email) ? string.Empty : savingAccount.Email,
                Mobile = string.IsNullOrWhiteSpace(savingAccount.Mobile) ? string.Empty : savingAccount.Mobile,
                NID = string.IsNullOrWhiteSpace(savingAccount.NID) ? string.Empty : savingAccount.NID
            };

            return accountStatementViewModel;
        }
    }
}