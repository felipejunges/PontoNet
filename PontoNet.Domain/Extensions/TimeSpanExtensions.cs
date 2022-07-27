namespace PontoNet.Domain.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string? ToTimeString(this TimeSpan? ts)
        {
            if (ts == null)
                return null;
            
            return ts.Value.ToTimeString();
        }

        public static string ToTimeString(this TimeSpan ts)
        {
            return new DateTime(ts.Ticks).ToString("HH:mm");
        }
    }
}