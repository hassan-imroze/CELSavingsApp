using CELSavings.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CELSavings.Models
{
    public class SavingAccount
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Account No cannot be empty")]
        [Index(IsUnique =true)]
        [StringLength(6,ErrorMessage ="Account No must be 6 letters long.",MinimumLength =6)]
        [CodeShouldBeUnique]
        public string AccountNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name cannot be empty")]
        [Index(IsUnique = true)]
        [StringLength(50,ErrorMessage ="Name cannot be greater than 50 letters")]
        [NameShouldBeUnique]
        public string Name { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }

        
        public string Mobile { get; set; }

        public string NID { get; set; }

        [DefaultValue((int)MemberStatus.Live)]
        public MemberStatus Status { get; set; }

        public decimal Balance { get; set; }

        public DateTime? LastPaymentMonthDate { get; set; }

        public DateTime? LastTransactionDate { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}