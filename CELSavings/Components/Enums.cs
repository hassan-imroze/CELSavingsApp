using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings
{
    public enum MemberStatus
    {
        Live = 1,
        Discontinued_Unsettled = 2,
        Discontinued = 3
    }

    public enum TransactionType
    {
        Payment = 1,
        Operating_Cost = 2,
        Small_Business = 3,
        Small_Business_Payment = 4,
        Loan_Return = 5,
        Investment = 6,
        Profit = 7,
    }

    public enum TransactionSide
    {
        Credit = 1,
        Debit = 2
        
    }
}