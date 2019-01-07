using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.ViewModels
{
    public class SmallBusinessPaymentViewModel
    {
        public int SmallBusinessId { get; set; }
        
        public string Product { get; set; }

        public string CustomerName { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal PaymentReceived { get; set; }
        
        public decimal PaymentDue { get; set; }

        public decimal PaidAmount { get; set; }
    }
}