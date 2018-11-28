using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.Dto
{
    public class PaymentDto
    {
        public int SavingAccountId { get; set; }

        public string SavingAccount { get; set; }
        
        public string PaymentMonth { get; set; }

        public decimal Amount { get; set; }

    }
}