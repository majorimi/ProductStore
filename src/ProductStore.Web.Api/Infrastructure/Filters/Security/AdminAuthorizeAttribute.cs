using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using ProductStore.Web.Api.Infrastructure.Security.Client;
using ProductStore.Web.Shared.Logging;

namespace ProductStore.Web.Api.Infrastructure.Filters.Security
{
	public class AdminAuthorizeAttribute : AuthorizeAttribute
	{
		public override bool AllowMultiple => false;

		protected override bool IsAuthorized(HttpActionContext actionContext)
		{
			return ClientContext.Current.Identity.IsAuthenticated && ClientContext.Current.Identity.IsAdmin;
		}

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (!IsAuthorized(actionContext))
			{
				LoggerContext.Current.Log.Warn($"Attempt to access to Admin resources: {actionContext.Request?.RequestUri} With token: {ClientContext.Current.Identity.ApiKey}. Request was unauthorized and rejected! If it occurs repeatedly it may suggest the system is under attack...");
				actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
			}
		}
	}
}