using System;

namespace Loris.Common.Extensions
{
    /// <summary>
    /// Extensão para manipulação de data hora
    /// </summary>
    public static class DateTimeExtension
    {
        /// <summary>
        /// Devolve a data com a hora determinada no primeiro minuto do dia
        /// </summary>
        /// <param name="date">data a utilizar de base</param>
        /// <returns></returns>
        public static DateTime FirstMinuteDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, (int)0, (int)0, (int)0);
        }

        /// <summary>
        /// Devolve a data com a hora determinada no último minuto do dia
        /// </summary>
        /// <param name="date">data a utilizar de base</param>
        /// <returns></returns>
        public static DateTime LastMinuteDate(this DateTime date)
        {
            var dateRes = new DateTime(date.Year, date.Month, date.Day, (int)0, (int)0, (int)0);

            return dateRes.AddDays(1).AddSeconds(-1);
        }
    }
}
