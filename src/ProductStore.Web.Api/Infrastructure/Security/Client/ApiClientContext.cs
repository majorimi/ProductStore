using System;
using System.IO;
using System.Text;
using System.Web;

namespace ProductStore.Web.Api.Infrastructure.Security.Client
{
	/// <summary>
	/// API Client context implementation
	/// </summary>
	public class ApiClientContext : ClientContext
	{
		protected static readonly Func<HttpContextBase> DefaultHttpContextProvider;

		private static Func<HttpContextBase> _getCurrentHttpContext;

		public static Func<HttpContextBase> GetCurrentHttpContext
		{
			get => _getCurrentHttpContext ?? DefaultHttpContextProvider;
			set => _getCurrentHttpContext = value;
		}

		static ApiClientContext()
		{
			DefaultHttpContextProvider = GetHttpContextDefault;
		}

		private static HttpContextBase GetHttpContextDefault()
		{
			if (HttpContext.Current != null)
			{
				return new HttpContextWrapper(HttpContext.Current);
			}

			return new HttpContextWrapper(CreateHttpContext());
		}

		public override IApiClient GetApiClient() => ((GetCurrentHttpContext().User as ApiClientPrincipal)?.Identity as IApiClient) ?? NullApiClient.Instance;

		private static HttpContext CreateHttpContext()
		{
			var sw = new StringWriter(new StringBuilder());
			var hres = new HttpResponse(sw);
			var hreq = new HttpRequest(string.Empty, "http://tempuri.org/", string.Empty);
			return new HttpContext(hreq, hres);
		}
	}
}