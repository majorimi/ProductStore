using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using ProductStore.Web.Models;
using ProductStore.Web.UI.Infrastructure.ApiClient;

namespace ProductStore.Web.UI.Infrastructure
{
	public class StoreApiService : IStoreApiService
	{
		private class ErrorResult
		{
			public string Message { get; set; }
		}

		private readonly IHttpClientProvider _httpClientProvider;

		public StoreApiService(IHttpClientProvider httpClientProvider)
		{
			_httpClientProvider = httpClientProvider;
		}

		public async Task<IEnumerable<ProductCategoryModel>> GetProductCategories(PagingModel paging, CancellationToken cancellationToken)
		{
			var uriBuilder = new UriBuilder(ConfigurationManager.AppSettings["ApiUrl"]);
			uriBuilder.Path += "ProductCategory";
			uriBuilder.Query = $"paging.pageSize={paging.PageSize}&paging.pageIndex={paging.PageIndex}";

			var ret = await GetAsync<IEnumerable<ProductCategoryModel>>(uriBuilder.ToString(),
				new MediaTypeWithQualityHeaderValue("application/json"), cancellationToken);

			if (ret.IsSuccess)
			{
				//TODO: log error, etc...
				return ret.Data;
			}

			return null;
		}

		public async Task<IEnumerable<ProductModel>> GetProducts(PagingModel paging, CancellationToken cancellationToken)
		{
			var uriBuilder = new UriBuilder(ConfigurationManager.AppSettings["ApiUrl"]);
			uriBuilder.Path += "Product";
			uriBuilder.Query = $"paging.pageSize={paging.PageSize}&paging.pageIndex={paging.PageIndex}";

			var ret = await GetAsync<IEnumerable<ProductModel>>(uriBuilder.ToString(),
				new MediaTypeWithQualityHeaderValue("application/json"), cancellationToken);

			if (ret.IsSuccess)
			{
				//TODO: log error, etc...
				return ret.Data;
			}

			return null;
		}

		public async Task<int> CreateProductAsync(CreateProductModel model, CancellationToken cancellationToken)
		{
			var uriBuilder = new UriBuilder(ConfigurationManager.AppSettings["ApiUrl"]);
			uriBuilder.Path += "Product";

			var ret = await SendAsync<int, CreateProductModel>(uriBuilder.ToString(), "POST", model,
				new MediaTypeWithQualityHeaderValue("application/json"), cancellationToken);

			if (ret.IsSuccess)
			{
				//TODO: log error, etc...
				return ret.Data;
			}

			return 0;
		}

		public async Task<bool> UpdateProductAsync(UpdateProductModel model, CancellationToken cancellationToken)
		{
			var uriBuilder = new UriBuilder(ConfigurationManager.AppSettings["ApiUrl"]);
			uriBuilder.Path += "Product";

			var ret = await SendAsync<bool, UpdateProductModel>(uriBuilder.ToString(), "PUT", model,
				new MediaTypeWithQualityHeaderValue("application/json"), cancellationToken);

			if (ret.IsSuccess)
			{
				//TODO: log error, etc...
				return ret.Data;
			}

			return false;
		}


		private async Task<ApiResult<T>> GetAsync<T>(string url, MediaTypeWithQualityHeaderValue accept, CancellationToken cancellationToken)
		{
			var message = new HttpRequestMessage(new HttpMethod("GET"), url);
			message.Headers.Accept.Add(accept);
			message.Headers.Add("Authorization", $"Bearer {""}");

			return await RequestAsync<T>(message, cancellationToken);
		}

		private async Task<ApiResult<T>> SendAsync<T, U>(string url, string method, U data, MediaTypeWithQualityHeaderValue accept, CancellationToken cancellationToken)
		{
			var message = new HttpRequestMessage(new HttpMethod(method), url);
			message.Headers.Accept.Add(accept);
			message.Headers.Add("Authorization", $"Bearer {""}");

			message.Content = new ObjectContent<U>(data, new JsonMediaTypeFormatter());

			return await RequestAsync<T>(message, cancellationToken);
		}

		private async Task<ApiResult<T>> RequestAsync<T>(HttpRequestMessage message, CancellationToken cancellationToken)
		{
			using (HttpResponseMessage response = await _httpClientProvider.HttpClient.SendAsync(message, cancellationToken))
			{
				using (HttpContent content = response.Content)
				{
					if (response.IsSuccessStatusCode)
					{
						var categories = await content.ReadAsAsync<T>();
						return ApiResult<T>.CreateSuccess((int)response.StatusCode, categories);
					}

					if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.BadRequest)
					{
						var error = await content.ReadAsAsync<ErrorResult>();
						return ApiResult<T>.CreateFailed((int)response.StatusCode, error.Message);
					}
					else
					{
						var error = await content.ReadAsStringAsync();
						return ApiResult<T>.CreateFailed((int)response.StatusCode, error);
					}
				}
			}
		}

	}
}