namespace ProductStore.Web.Api.Infrastructure.Security.Client
{
	/// <summary>
	/// Default ApiClient user implementation for anonymous
	/// </summary>
	public class NullApiClient : IApiClient
	{
		public static NullApiClient Instance { get; }

		static NullApiClient()
		{
			Instance = new NullApiClient();
		}

		private NullApiClient()
		{ }

		public string Name => "Anonymous";

		public string AuthenticationType => string.Empty;

		public bool IsAuthenticated => false;

		public int ClientId => 0;

		public string ClientName => Name;

		public int ApiKeyId => 0;

		public string ApiKey => string.Empty;

		public bool IsAdmin => false;

		public override string ToString()
		{
			return $"Name: {Name}, IsAuthenticated: {IsAuthenticated}";
		}
	}
}