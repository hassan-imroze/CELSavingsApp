using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.Dto
{
    public class PayableSavingAccountDto
    {
    
        public int SavingsAccountId { get; set; }

        public string Name { get; set; }

       
        public string PaymentMonth { get; set; }
    }
}