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
        Loan_Return = 2,
        Investment = 3,
        Profit = 4,
        Other_Deduction = 5
    }

    public enum TransactionSide
    {
        Debit = 1,
        Credit = 2
        
    }
}