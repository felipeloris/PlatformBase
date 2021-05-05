using Loris.Common.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Loris.Common.Log
{
    public class InternalLog : IInternalLog
    {
        protected Type _appType = null;
        protected ILogger _logger = null;

        public void LogError(Exception e)
        {
            _logger.LogError(e, _appType.FullName);
        }

        public void LogError(string method, Exception e)
        {
            _logger.LogError(e, $"{_appType.FullName} - {method}");
        }

        public void LogError(Type classError, string method, Exception e)
        {
            _logger.LogError(e, $"{classError.FullName}::{method}");
        }

        public void LogInfo(string logEntry)
        {
            _logger.LogInformation($"{_appType.FullName} - {logEntry}");
        }

        public void LogDebug(string logEntry)
        {
            _logger.LogDebug($"{_appType.FullName} - {logEntry}");
        }

        public void LogDebugStart(string method)
        {
            LogDebug($"{_appType.FullName} - Início da rotina {method}");
        }

        public void LogDebugStart()
        {
            LogStackTraceDebug("Início da rotina");
        }

        public void LogDebugFinish(string method)
        {
            LogDebug($"{_appType.FullName} - Fim da rotina {method}");
        }

        public void LogDebugFinish()
        {
            LogStackTraceDebug("Fim da rotina");
        }

        public void LogStackTraceDebug(string message)
        {
            var st = new StackTrace(new StackFrame(1));
            var methodName = string.Concat(st.GetFrame(0).GetMethod().Name, ": ");
            var type = st.GetFrame(0).GetType();

            LogDebug($"{type.FullName} :: {methodName} - {message}");
        }

        public void LogStackTraceError(Exception ex)
        {
            var st = new StackTrace(new StackFrame(1));
            var methodName = string.Concat(st.GetFrame(0).GetMethod().Name, ": ");
            var type = st.GetFrame(0).GetType();

            LogError($"{type.FullName} :: {methodName}", ex);
        }
    }
}
