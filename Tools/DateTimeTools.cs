using System.Globalization;

namespace DoonGPay.Tools
{
    public static class DateTimeTools
    {
        public static string ToPersianDateString(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            if (date < new DateTime(622, 3, 22) && date > new DateTime(9999, 12, 31))
                return null ;
            else
                return $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
        }
    }
}
