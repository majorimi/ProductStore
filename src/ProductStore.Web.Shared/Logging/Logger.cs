using System;
using log4net;

namespace ProductStore.Web.Shared.Logging
{
	public class Logger : ILogger
	{
		private readonly ILog _logger;

		public Logger()
		{
			_logger = LogManager.GetLogger(typeof(Logger));
		}

		public Logger(string name)
		{
			_logger = LogManager.GetLogger(name);
		}

		public void Error(Exception ex)
		{
			_logger.Error(ex);
		}

		public void Error(string message, Exception ex)
		{
			_logger.Error(message, ex);
		}

		public void Error(string message)
		{
			_logger.Error(message);
		}

		public void Warn(string message)
		{
			_logger.Warn(message);
		}

		public void Info(string message)
		{
			_logger.Info(message);
		}
	}
}