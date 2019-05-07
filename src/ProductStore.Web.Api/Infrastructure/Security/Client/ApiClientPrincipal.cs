using System.Security.Claims;
using System.Security.Principal;

namespace ProductStore.Web.Api.Infrastructure.Security.Client
{
	/// <summary>
	/// API Client principal with <see cref="IApiClient"/> identity
	/// </summary>
	public class ApiClientPrincipal : ClaimsPrincipal
	{
		private readonly IApiClient _identity;

		public override IIdentity Identity => _identity;

		public ApiClientPrincipal(IApiClient wellEzUser)
			: base(wellEzUser)
		{
			_identity = wellEzUser;
		}
	}
}