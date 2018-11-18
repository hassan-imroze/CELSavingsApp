using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using CELSavings.Models;

namespace CELSavings.Repository
{
    public class SavingAccountRepository : IDisposable
    {

        #region Initialize

        private ApplicationDbContext _context;
        public SavingAccountRepository()
        {
            _context = new ApplicationDbContext();
        }

        #endregion

        #region Functions

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


        public void Save(SavingAccount savingAccount)
        {
            if (savingAccount.Id == 0)
            {
                savingAccount.Balance = 0;
                savingAccount.LastPaymentDate = null;
                savingAccount.LastTransactionDate = null;
                _context.SavingAccounts.Add(savingAccount);
            }
            else
            {
                var savingAccountInDb = _context.SavingAccounts.FirstOrDefault(x => x.Id == savingAccount.Id);
                savingAccountInDb.Name = savingAccount.Name;
                savingAccountInDb.AccountNo = savingAccount.AccountNo;
                savingAccountInDb.Email = savingAccount.Email;
            }

            _context.SaveChanges();
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }
                _context.Dispose();
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SavingAccountRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}