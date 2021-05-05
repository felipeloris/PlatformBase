using System;
using Loris.Common.Log;

namespace Loris.Common.Exceptions
{
    public class BusinessExceptionHandler
    {
        private static readonly LogManager<BusinessExceptionHandler> Logger = new LogManager<BusinessExceptionHandler>();

        public static bool HandleException(Type classError, string method, ref Exception ex)
        {
            if (ex is DataException)
            {
                var checkError = (ex as DataException).CheckError;

                if (checkError != null)
                {
                    // Foi decidido passar uma exceção de negócio, ao invés de exceções sem mensagem
                    if (checkError.ErrorType == DataErrorTypeEnum.UniqueViolation)
                        ex = new BusinessException(Messages.UniqueViolation());

                    if (checkError.ErrorType == DataErrorTypeEnum.ForeignKeyViolation)
                        ex = new BusinessException(Messages.DbOperationViolation());

                    if (checkError.ErrorType == DataErrorTypeEnum.ExclusionViolation)
                        ex = new BusinessException(Messages.DbOperationViolation());
                }
            }

            if (!(ex is BusinessException))
            {
                var guid = Guid.NewGuid();
                Logger.LogError(classError, $"{method} - GUID={guid}", ex);
                
                ex = new TreatedErrorException(string.Format(Messages.SystemError(guid), guid));
            }

            return true;
        }
    }
}
