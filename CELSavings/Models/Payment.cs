using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CELSavings.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int SavingAccountId { get; set; }

        public SavingAccount SavingAccount { get; set; }

        [Required]
        public DateTime PaymentMonthDate { get; set; }

        [Required]
        public string PaymentMonth { get; set; }
        
        public decimal Amount { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

    }
}