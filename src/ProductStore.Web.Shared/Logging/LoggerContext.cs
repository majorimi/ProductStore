namespace ProductStore.Web.Shared.Logging
{
	public abstract class LoggerContext
	{
		public static LoggerContext Current { get; set; }

		public abstract ILogger Log { get; }
	}
}