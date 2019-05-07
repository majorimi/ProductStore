namespace ProductStore.Web.Api.Infrastructure.Security.Client
{
	/// <summary>
	/// Client Context base class
	/// </summary>
	public abstract class ClientContext
	{
		private static readonly ClientContext Default = new DeafultClientContext();

		private static ClientContext _current;

		public static ClientContext Current
		{
			get => _current ?? (_current = Default);
			set => _current = value ?? Default;
		}

		public IApiClient Identity => GetApiClient();

		public abstract IApiClient GetApiClient();
	}
}