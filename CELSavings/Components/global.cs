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
    }
}