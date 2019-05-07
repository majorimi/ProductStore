using System.Reflection;

namespace ProductStore.Web.Shared.Logging
{
	public class DefaultLogger : LoggerContext
	{
		private readonly ILogger _logger;

		public DefaultLogger()
		{
			_logger = new Logger(Assembly.GetExecutingAssembly().GetName().Name);
		}

		public override ILogger Log => _logger;
	}
}