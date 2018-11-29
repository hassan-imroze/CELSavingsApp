using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using CELSavings.Models;

namespace CELSavings.Repository
{
    public class SavingAccountRepository : RepositoryBase
    {
        
        public SavingAccountRepository(): base() { }
        
        public bool IsAccountNumberAlreadyExists(string accountNo, int Id = 0)
        {
            bool exists = false;
            if (!string.IsNullOrWhiteSpace(accountNo))
            {
                exists = _context.SavingAccounts.Any(x => x.AccountNo.ToUpper().Trim() == accountNo.ToUpper().Trim()
                                                          && x.Id != Id);
            }
            return exists;


        }

        public bool IsNameAlreadyExists(string name, int Id = 0)
        {
            bool exists = false;
            if (!string.IsNullOrWhiteSpace(name))
            {
                exists = _context.SavingAccounts.Any(x => x.Name.ToUpper().Trim() == name.ToUpper().Trim()
                                                          && x.Id != Id);
            }
            return exists;
        }

        public SavingAccount GetById(int Id)
        {
            return _context.SavingAccounts.FirstOrDefault(x => x.Id == Id);
        }

        public List<SavingAccount> GetSavingsAccounts(string query = null)
        {
            var savingsAccountQuery = _context.SavingAccounts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query))
            {
                 savingsAccountQuery = savingsAccountQuery.Where(x => x.Name.Contains(query));
            }
            return savingsAccountQuery.ToList();
        }

        public List<SavingAccount> GetPayableSavingsAccounts(string query = null)
        {
            
            var currentPaymentMonth = DateTime.Today.FirstDateOfMonth();
            var savingsAccountQuery = _context.SavingAccounts.Where(x => x.LastPaymentMonthDate == null || x.LastPaymentMonthDate < currentPaymentMonth);

            if (!string.IsNullOrWhiteSpace(query))
            {
                savingsAccountQuery = savingsAccountQuery.Where(x => x.Name.Contains(query));
            }
            return savingsAccountQuery.ToList();
        }

        public void Save(SavingAccount savingAccount)
        {
            if (savingAccount.Id == 0)
            {
                savingAccount.Balance = 0;
                savingAccount.LastPaymentMonthDate = null;
                savingAccount.LastTransactionDate = null;
                savingAccount.Status = MemberStatus.Live;
                _context.SavingAccounts.Add(savingAccount);
            }
            else
            {
                var savingAccountInDb = _context.SavingAccounts.FirstOrDefault(x => x.Id == savingAccount.Id);
                savingAccountInDb.Name = savingAccount.Name;
                savingAccountInDb.AccountNo = savingAccount.AccountNo;
                savingAccountInDb.Email = savingAccount.Email;
                savingAccountInDb.Mobile = savingAccount.Mobile;
                savingAccountInDb.NID = savingAccount.NID;
            }

            _context.SaveChanges();
        }

        
        
    }
}