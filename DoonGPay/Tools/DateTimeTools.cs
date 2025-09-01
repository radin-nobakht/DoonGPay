using System.Globalization;

namespace DoonGPay.Tools
{
    public static class DateTimeTools
    {
        public static string ToPersianDateString(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
        }
    }
}
