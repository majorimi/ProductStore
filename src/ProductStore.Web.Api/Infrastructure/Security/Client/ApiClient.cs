using System;
using System.Linq;
using System.Security.Claims;

namespace ProductStore.Web.Api.Infrastructure.Security.Client
{
	/// <summary>
	/// API Client implementation for authenticated users
	/// </summary>
	public sealed class ApiClient : ClaimsIdentity, IApiClient
	{
		public override bool IsAuthenticated => true;

		public override string AuthenticationType { get; }

		public int ClientId => Convert.ToInt32(Claims.FirstOrDefault(c => c.Type == nameof(ClientId))?.Value);
		public string ClientName => Name;
		public int ApiKeyId => Convert.ToInt32(Claims.FirstOrDefault(c => c.Type == nameof(ApiKeyId))?.Value);
		public string ApiKey => Claims.FirstOrDefault(c => c.Type == nameof(ApiKey))?.Value;
		public bool IsAdmin => Convert.ToBoolean(Claims.FirstOrDefault(c => c.Type == nameof(IsAdmin))?.Value);

		public ApiClient(string authenticationType,
			int clientId,
			string clientName,
			int apiKeyId,
			string apiKey,
			bool isAdmin)
		{
			AddClaim(new Claim(ClaimTypes.Name, clientName));
			AddClaim(new Claim(ClaimTypes.AuthenticationMethod, authenticationType));
			AuthenticationType = authenticationType;

			AddClaim(new Claim(nameof(ClientId), clientId.ToString()));
			AddClaim(new Claim(nameof(ApiKeyId), apiKeyId.ToString()));
			AddClaim(new Claim(nameof(ApiKey), apiKey));
			AddClaim(new Claim(nameof(IsAdmin), isAdmin.ToString()));
		}

		public override string ToString()
		{
			return
				$"Name: {Name}, IsAuthenticated: {IsAuthenticated}, ClientId: {ClientId}, ClientName: {ClientName}, ApiKeyId: {ApiKeyId}, ApiKey: {ApiKey}, IsAdmin: {IsAdmin}";
		}
	}
}