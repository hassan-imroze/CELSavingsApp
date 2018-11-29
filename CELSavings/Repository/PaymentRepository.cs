using CELSavings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.Repository
{
    public class PaymentRepository : RepositoryBase
    {
        public PaymentRepository() : base() { }

        internal void Create(Payment payment)
        {
           var savingsAccount = _context.SavingAccounts.FirstOrDefault(x => x.Id == payment.SavingAccountId);
            if (savingsAccount == null)
                throw new Exception("Savings account was not present");
            payment.PaymentMonthDate = savingsAccount.LastPaymentMonthDate == null ?
                                       GlobalConstants.STSTEMSTARTMONTH.LastDateOfMonth() :
                                       savingsAccount.LastPaymentMonthDate.Value.AddDays(1).LastDateOfMonth();
            payment.PaymentMonth = payment.PaymentMonthDate.FormattedMonth();
            payment.Transactions = new List<Transaction>
            {
                new Transaction
                {
                    SavingAccountId = payment.SavingAccountId,
                    Amount = payment.Amount,
                    Description = string.Format("Payment of month: {0}",payment.PaymentMonth),
                    TransactionDate = DateTime.Today,
                    Type = TransactionType.Payment,
                    TransactionSide = TransactionSide.Debit  
                }
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();
        }
    }
}