namespace EventManager.Web.Infrastructure.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        public static string ToFriendlyDateTime(this DateTime date)
            => $"{date:MM/dd/yy H:mm:ss}";
    }
}
