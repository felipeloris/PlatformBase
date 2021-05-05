using System;

namespace Loris.Common.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}
