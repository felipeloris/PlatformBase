using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Loris.Common.Log
{
    public class LogManager<T> : InternalLog
    {
		public LogManager()
		{
			var serviceProvider = new ServiceCollection().BuildServiceProvider();
			_logger = serviceProvider.GetService<ILogger<T>>();
            _appType = typeof(T);

            if (_logger == null)
            {

				serviceProvider = new ServiceCollection()
					.AddLogging(cfg => cfg.AddConsole())
					.Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug)
					.BuildServiceProvider();
			}
			_logger = serviceProvider.GetService<ILogger<T>>();
		}
    }

    public class LogManager : LogManager<object>
    {
        public LogManager(Type appType) : base()
        {
            _appType = appType;
        }
    }
}
