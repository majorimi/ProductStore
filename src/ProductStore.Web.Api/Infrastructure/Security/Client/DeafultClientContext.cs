namespace ProductStore.Web.Api.Infrastructure.Security.Client
{
	/// <summary>
	/// Default <see cref="ClientContext"/> implementation sends back <see cref="NullApiClient"/>
	/// </summary>
	public class DeafultClientContext : ClientContext
	{
		public override IApiClient GetApiClient()
		{
			return NullApiClient.Instance;
		}
	}
}