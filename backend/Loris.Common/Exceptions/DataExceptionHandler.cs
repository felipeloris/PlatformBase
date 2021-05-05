using System;
using Loris.Common.Log;

namespace Loris.Common.Exceptions
{
    public class DataExceptionHandler
    {
        private static readonly LogManager<DataExceptionHandler> Logger = new LogManager<DataExceptionHandler>();

        public static DataCheckError DataCheckError { get; set; } = new DataCheckError();

        public static bool HandleException(Type classError, string method, ref Exception ex)
        {
            if (!(ex is DataException))
            {
                var checkError = DataCheckError.GetNewCheckError(ex);
                ex = new DataException(checkError);
            }
            Logger.LogError(classError, method, ex);

            return true;
        }
    }
}
