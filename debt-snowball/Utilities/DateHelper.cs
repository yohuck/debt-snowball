namespace debt_snowball.Utilities
{
using System;
using System.Globalization;

public class DateHelper
{
    public static string AddMonths(string dateString, int months)
    {
        DateTime date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        int originalDay = date.Day;

        date = date.AddMonths(months);

        if (date.Day != originalDay)
        {
            date = new DateTime(date.Year, date.Month, 1).AddDays(-1);
        }

        return date.ToString("yyyy-MM-dd");
    }
}

}
