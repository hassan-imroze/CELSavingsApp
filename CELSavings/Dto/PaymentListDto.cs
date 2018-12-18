using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.Dto
{
    public class PaymentListDto
    {
        public string AccountNo { get; set; }

        public string PaymentMonth { get; set; }

        public decimal PaymentAmount { get; set; }
    }
}