using System;
using System.Linq;
using Loris.Common.Extensions;

namespace Loris.Common.Helpers
{
    public static class DateHelper
    {
        public static DateTime Converter(string valor)
        {
            var data = DateTime.Now;
            var ok = DateTime.TryParse(valor, out data);

            return data;
        }

        public static DateTime GetFirstDay(DateTime dt) =>
            new DateTime(dt.Year, dt.Month, 1);

        public static DateTime GetLastDay(DateTime dt) =>
            new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));

        public static DateTime GetFirstDayFromThisMonth() =>
            GetFirstDay(DateTime.Now);

        public static DateTime GetLastDayFromThisMonth() =>
            GetLastDay(DateTime.Now);

        public static DateTime? SetLastHour(DateTime? dateTime)
        {
            var onlyDate = dateTime?.Date;
            return onlyDate?.AddDays(1).AddSeconds(-1);
        }

        public static bool ValidateDate(string text)
        {
            if (string.IsNullOrEmpty(text.Trim()))
            {
                return false;
            }

            try
            {
                var x = DateTime.Parse(text);

                if (x.Year < 1800 || x.Year > 2050)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool ValidateHour(string text)
        {
            if (!string.IsNullOrEmpty(text.Trim()))
                try
                {
                    var tm = DateTime.Now.TimeOfDay;

                    return TimeSpan.TryParse(text, out tm);
                }
                catch (Exception)
                {
                    return false;
                }

            return true;
        }

        public static long ConvertDateTimeToLong(DateTime dt)
        {
            return long.Parse(dt.ToString("yyyyMMddHHmmss"));
        }

        public static DateTime ConvertLongToDateTime(long? lng)
        {
            if (lng == null)
                return DateTime.MinValue;

            try
            {
                //yyyyMMddHHmmss
                //00000000001111
                //01234567890123
                var strDt = ('0'.Repeat(14) + lng.ToString()).Right(14);
                var year = int.Parse(strDt.Substring(0, 4));
                var month = int.Parse(strDt.Substring(4, 2));
                var day = int.Parse(strDt.Substring(6, 2));
                var hour = int.Parse(strDt.Substring(8, 2));
                var min = int.Parse(strDt.Substring(10, 2));
                var sec = int.Parse(strDt.Substring(12, 2));

                return new DateTime(year, month, day, hour, min, sec);
            }
            catch 
            {
                return DateTime.MinValue;
            }
        }
    }
}
