using Loris.Common.Domain.Interfaces;
using System;
using System.Data.Common;

namespace Loris.Common.Exceptions
{
    /// <summary>
    /// Exception na camada de acesso a dados - DAL
    /// </summary>
    public class DataException : DbException
    {
        public IDataCheckError CheckError { get; protected set; }

        public DataException(string message)
           : base(message)
        {
            CheckError = new DataCheckError().GetNewCheckError(new Exception(message));
        }

        public DataException(string message, Exception inner)
           : base(message, inner)
        {
            CheckError = new DataCheckError().GetNewCheckError(inner);
        }

        public DataException(IDataCheckError dataCheckError)
           : base(dataCheckError.ErrorMessage, dataCheckError.InternalException)
        {
            CheckError = dataCheckError;
        }
    }
}
