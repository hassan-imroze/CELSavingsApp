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

        public List<AccountStatementDetailViewModel> Details { get; set; }

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

            accountStatementViewModel.Details = new List<AccountStatementDetailViewModel>();

            int serial = 0;
            decimal runningTotal = 0;
            foreach (var item in savingAccount.Transactions.OrderBy(x => x.TransactionDate))
            {
                runningTotal += item.TransactionSide == TransactionSide.Credit ? item.Amount : (-item.Amount);
                accountStatementViewModel.Details.Add(new AccountStatementDetailViewModel
                {
                    Serial = ++serial,
                    TranDate = item.TransactionDate.FormattedDate(),
                    Description = item.Description,
                    Debit = item.TransactionSide == TransactionSide.Debit ? item.Amount : 0,
                    Credit = item.TransactionSide == TransactionSide.Credit ? item.Amount : 0,
                    Balance = runningTotal
                });
            }

            return accountStatementViewModel;
        }
    }

    public class AccountStatementDetailViewModel
    {
        public int Serial { get; set; }
        
        public string TranDate { get; set; }

        public string Description { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }
        
        public decimal Balance { get; set; }
        
    }
}