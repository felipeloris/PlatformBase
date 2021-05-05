using System;

namespace Loris.Common.Exceptions
{
    public class TreatedErrorException : ApplicationException
    {
        public TreatedErrorException(string message)
            : base(message)
        {
        }

        public TreatedErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
