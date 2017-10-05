using System;
namespace Nc.Erp.WorksSupport.Api.Common
{
    public class DateTimeHelper
    {
        //compare the valueToCompare with current datetime. return tru if valueToCompare is bigger than current datetime
        public static bool IsExpired(DateTime utcValueToCompare)
        {
            DateTime currentUtc = GetUtcDateTime();
            bool result = DateTime.Compare(currentUtc, utcValueToCompare) >= 0;
            return result;
        }

        private static DateTime GetUtcDateTime()
        {
            return DateTime.UtcNow;
        }

        public static DateTime GetAuthenticationTokenExpiredUtcDateTime(int minute)
        {
            return DateTime.UtcNow.AddMinutes(minute);
        }
    }
}