using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CELSavings.Models
{
    public class SavingAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public decimal Balance { get; set; }

        public DateTime? LastPaymentDate { get; set; }

    }
}