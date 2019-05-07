using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ProductStore.Web.Api.Infrastructure.Security.Client;

namespace ProductStore.Web.Api.Infrastructure.Security
{
	public class BearerAuthenticationMessageHandler : DelegatingHandler
	{
		private const string AuthType = "Bearer";

		public BearerAuthenticationMessageHandler()
		{
		}

		/// <summary>
		/// Checks ApiKeys in the incoming HTTP Requests and authenticating the context
		/// </summary>
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request.Headers.Authorization != null)
			{
				try
				{
					var identity = await GetClientAsync(request, cancellationToken);

					if (identity != null)
					{
						var principal = new ApiClientPrincipal(identity);

						Thread.CurrentPrincipal = principal;
						if (HttpContext.Current != null)
						{
							HttpContext.Current.User = principal;
						}
					}
				}
				catch (UnauthorizedAccessException)
				{
					//Move to the next handler Authorize attribute should handle the request
				}
			}

			return await base.SendAsync(request, cancellationToken);
		}

		private async Task<IApiClient> GetClientAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request.Headers.Authorization?.Scheme?.Equals(AuthType, StringComparison.OrdinalIgnoreCase) ?? false)
			{
				//TODO: validate token...
			}

			return null;
		}
	}
}