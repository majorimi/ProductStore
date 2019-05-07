using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProductStore.Web.Models;
using ProductStore.Web.UI.Infrastructure.ApiClient;
using ProductStore.Web.UI.Models.Home;

namespace ProductStore.Web.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IHttpClientProvider _httpClientProvider;

		public HomeController(IHttpClientProvider httpClientProvider)
		{
			_httpClientProvider = httpClientProvider;
		}

		public async Task<ActionResult> Index(CancellationToken cancellationToken)
		{
			var model = new HomeModel();

			var uriBuilder = new UriBuilder(ConfigurationManager.AppSettings["ApiUrl"]);
			uriBuilder.Path += "ProductCategory";
			uriBuilder.Query = "paging.pageSize=50&paging.pageIndex=1";

			var message = new HttpRequestMessage(new HttpMethod("GET"), uriBuilder.ToString());
			message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			message.Headers.Add("Authorization", $"Bearer {""}");

			//message.Content = new ObjectContent<ProductCategoryModel>(data, new JsonMediaTypeFormatter());

			using (HttpResponseMessage response = await _httpClientProvider.HttpClient.SendAsync(message, cancellationToken))
			{
				using (HttpContent content = response.Content)
				{
					if (response.IsSuccessStatusCode)
					{
						var categories = await content.ReadAsAsync<IEnumerable<ProductCategoryModel>>();
						model.ProductCategories = categories;
					}
					//else if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.BadRequest)
					//{
					//	return LabelResult.CreateFailed(uriBuilder.ToString(), (int)response.StatusCode, await content.ReadAsStringAsync());
					//}
					//else
					//{
					//	var error = await content.ReadAsAsync<ApiErrorResponse>();
					//	return LabelResult.CreateFailed(uriBuilder.ToString(), (int)response.StatusCode, error.Message);
					//}
				}
			}

			return View(model);
		}
	}
}