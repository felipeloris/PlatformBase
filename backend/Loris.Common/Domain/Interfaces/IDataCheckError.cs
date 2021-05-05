using System;

namespace Loris.Common.Domain.Interfaces
{
    public interface IDataCheckError
    {
        bool IsDefined { get; }
        
        DataErrorTypeEnum ErrorType { get; }
        
        string ErrorMessage { get; } 

        Exception InternalException { get; } 

        IDataCheckError GetNewCheckError(Exception ex);
    }
}
