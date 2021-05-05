using System;

namespace Loris.Common.Domain.Interfaces
{
    public interface IInternalLog 
    {
        void LogError(Exception e);

        void LogError(string method, Exception e);

        void LogError(Type classError, string method, Exception e);

        void LogInfo(string logEntry);

        void LogDebug(string logEntry);

        void LogDebugStart(string method);

        void LogDebugStart();

        void LogDebugFinish(string method);

        void LogDebugFinish();

        void LogStackTraceDebug(string message);

        void LogStackTraceError(Exception ex);
    }
}
