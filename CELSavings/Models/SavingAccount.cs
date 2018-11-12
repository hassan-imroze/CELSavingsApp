using System;
using System.Collections.Generic;
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
        public string AccountNo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name cannot be empty")]
        [Index(IsUnique = true)]
        [StringLength(50,ErrorMessage ="Name cannot be greater than 50 letters")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress,ErrorMessage ="Email address is invaid")]
        public string Email { get; set; }
        
        public decimal Balance { get; set; }

        public DateTime? LastPaymentDate { get; set; }

        public DateTime? LastTransactionDate { get; set; }
    }
}