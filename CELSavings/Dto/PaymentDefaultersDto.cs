using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.Dto
{
    public class PaymentDefaultersDto
    {
        public int SavingsAccountId { get; set; }

        public string AccountNo { get; set; }

        public string Name { get; set; }
        
        public string NotPaidMonth { get; set; }
    }
}