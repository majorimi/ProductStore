using System.Net.Http;

namespace ProductStore.Web.UI.Infrastructure.ApiClient
{
	public interface IHttpClientProvider
	{
		HttpClient HttpClient { get; }
	}
}