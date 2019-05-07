using System.Security.Principal;

namespace ProductStore.Web.Api.Infrastructure.Security.Client
{
	/// <summary>
	/// API client interface with all the information about a Client
	/// </summary>
	public interface IApiClient : IIdentity
	{
		int ClientId { get; }
		string ClientName { get; }
		int ApiKeyId { get; }
		string ApiKey { get; }
		bool IsAdmin { get; }
	}
}