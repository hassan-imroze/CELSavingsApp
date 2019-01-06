using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.Dto
{
    public class SmallBusinessListDto
    {
        public int Id { get; set; }

        public string Product { get; set; }

        public string CustomerName { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal PaymentReceived { get; set; }

        public string StatusColor { get; set; }

        public string Status { get; set; }

        public string DueDate { get; set; }

    }
}