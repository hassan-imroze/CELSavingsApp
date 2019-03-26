using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CELSavings.Models
{
    public class SmallBusiness
    {
        public int Id { get; set; }

        [Required]
        public string Product { get; set; }

        public string ProductDescription { get; set; }

        
        
        [Required]
        public int CustomerOrGuarantorId { get; set; }

        public SavingAccount CustomerOrGuarantor { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        [Required]
        public decimal BuyingPrice { get; set; }

        [Required]
        public decimal ProfitPercentage { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }

        [Required]
        public DateTime SellDate { get; set; }

        public decimal InitialPayment { get; set; }

        public decimal PaymentReceived { get; set; }

        public DateTime InstallmentStartDate { get; set; }

        public DateTime? PaymentDueDate { get; set; }

        public List<SmallBusinessSavingAccount> InvolvedSavingAccounts { get; set; }

        public List<Transaction> Transactions { get; set; }

        public List<SmallBusinessInstallment> Installments { get; set; }

        public List<SmallBusinessPayment> Payments { get; set; }

    }

    public class SmallBusinessSavingAccount
    {
        public int Id { get; set; }

        [Required]
        public int SmallBusinessId { get; set; }

        public SmallBusiness SmallBusiness { get; set; }

        [Required]
        public int SavingAccountId { get; set; }

        public SavingAccount SavingAccount { get; set; }
    }

    public class SmallBusinessInstallment
    {
        public int Id { get; set; }

        [Required]
        public int SmallBusinessId { get; set; }

        public SmallBusiness SmallBusiness { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

    }

    public class SmallBusinessPayment
    {
        public int Id { get; set; }

        [Required]
        public int SmallBusinessId { get; set; }

        public SmallBusiness SmallBusiness { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}