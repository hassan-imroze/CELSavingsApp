using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CELSavings.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public int SavingAccountId { get; set; }

        public SavingAccount SavingAccount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int ParentId { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public TransactionSide TransactionSide { get; set; }
        
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public int? PaymentId { get; set; }

        public Payment Payment { get; set; }

    }
}