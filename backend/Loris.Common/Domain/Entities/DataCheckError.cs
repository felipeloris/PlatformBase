using Loris.Common.Domain.Interfaces;
using System;

namespace Loris.Common.Exceptions
{
    public class DataCheckError : IDataCheckError
    {
        public bool IsDefined { get; protected set; } = false;
        public DataErrorTypeEnum ErrorType { get; protected set; } = DataErrorTypeEnum.Undefined;
        public string ErrorMessage { get; protected set; } = "Undefined";
        public Exception InternalException { get; protected set; } = null;

        public DataCheckError()
        {
        }

        public DataCheckError(bool isDefined, DataErrorTypeEnum errorType, string errorMessage, Exception internalException)
        {
            IsDefined = isDefined;
            ErrorType = errorType;
            ErrorMessage = errorMessage;
            InternalException = internalException;
        }

        public virtual IDataCheckError GetNewCheckError(Exception ex)
        {
            return new DataCheckError()
            {
                ErrorMessage = ex.Message,
                InternalException = ex
            };
        }
    }
}
