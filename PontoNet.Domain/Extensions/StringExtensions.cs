using System.Globalization;

namespace PontoNet.Domain.Extensions
{
    public static class StringExtensions
    {
        public static TimeSpan ToDateTime(this string time)
        {
            string format = "HH:mm";

            DateTime.TryParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var outTime);

            return outTime.TimeOfDay;
        }

        public static bool ValidateDateTime(this string time)
        {
            string format = "HH:mm";

            return DateTime.TryParseExact(time, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var outTime);
        }
    }
}