using System.Net.Http;

namespace ProductStore.Web.UI.Infrastructure.ApiClient
{
	public class SimpleHttpClientProvider : IHttpClientProvider
	{
		private static readonly HttpClient _httpClient;

		static SimpleHttpClientProvider()
		{
			_httpClient = new HttpClient();
		}

		public HttpClient HttpClient => _httpClient;
	}
}