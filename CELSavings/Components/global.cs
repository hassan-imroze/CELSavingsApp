using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CELSavings
{
    public static class GlobalConstants
    {
        public static readonly DateTime STSTEMSTARTMONTH = DateTime.Parse("30 Nov 2018");
    }

    public static class RoleName
    {
        public const string CanManageSavingAccounts = "CanManageSavingAccounts";
    }

    public static class ExtentionMethods
    {
        public static DateTime FirstDateOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDateOfMonth(this DateTime date)
        {
            return date.FirstDateOfMonth().AddMonths(1).AddDays(-1);
        }

        public static string FormattedMonth(this DateTime date)
        {
            return date.ToString("MMM yyyy");
        }

        public static string FormattedDate(this DateTime date)
        {
            return date.ToString("dd MMM yyyy");
        }
    }

    public static class GlobalFunctions
    {
        public static List<Installment> CreateInstallments(decimal totalAmount, DateTime startDate)
        {
            List<Installment> installments = new List<Installment>();
            decimal installmentAmount = 0;
            DateTime currentDate = startDate;

            if(totalAmount > 30000 && totalAmount < 100000)
            {
                installmentAmount = 10000;

                while (totalAmount > installmentAmount)
                {
                    installments.Add(new Installment
                    {
                        Amount = installmentAmount,
                        DueDate = currentDate
                    });
                    currentDate = currentDate.AddMonths(1);
                    totalAmount -= installmentAmount;
                }
                if(totalAmount>0)
                {
                    installments.Add(new Installment
                    {
                        Amount = totalAmount,
                        DueDate = currentDate
                    });
                }
            }
            else
            {
                int installmentCount = (totalAmount < 30000) ? 3 : 10;
                installmentAmount = totalAmount / (decimal)installmentCount;
                for (int i = 0; i < installmentCount; i++)
                {
                    installments.Add(new Installment
                                    {
                                        Amount = installmentAmount,
                                        DueDate = currentDate
                                    });
                    currentDate = currentDate.AddMonths(1);
                }
            }
            

            return installments;
        }
    }

    public class Installment
    {
        public DateTime DueDate { get; set; }

        public decimal Amount { get; set; }
    }
}