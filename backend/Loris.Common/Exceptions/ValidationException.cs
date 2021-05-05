using System;

namespace Loris.Common.Exceptions
{
    public class ValidationException : ApplicationException
    {
        private ValidationsExceptionType _type = ValidationsExceptionType.Undefined;

        public ValidationException(ValidationsExceptionType type)
        {
            _type = type;
        }

        public ValidationException(ValidationsExceptionType type, string message) 
            : base(message)
        {
            _type = type;
        }
    }
}
