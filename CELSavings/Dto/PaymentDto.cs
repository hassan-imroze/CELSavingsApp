using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.Dto
{
    public class PaymentDto
    {
        public int SavingsAccountId { get; set; }
        
        public decimal Amount { get; set; }

    }
}