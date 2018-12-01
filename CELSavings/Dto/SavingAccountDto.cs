using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings.Dto
{
    public class SavingAccountDto
    {
        public int Id { get; set; }

        public string AccountNo { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public decimal Balance { get; set; }
    }
}