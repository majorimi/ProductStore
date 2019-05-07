using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Results;

namespace ProductStore.Web.Api.Infrastructure.Extensions
{
	public static class HttpResponseExtensions
	{
		public const string StreamMediaTypeValue = "application/octet-stream";

		public static IHttpActionResult CreateApiFileResponse(this HttpRequestMessage request, string fileName, byte[] data)
		{
			var result = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new ByteArrayContent(data)
			};
			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = fileName
			};
			result.Content.Headers.ContentType = new MediaTypeHeaderValue(StreamMediaTypeValue);
			result.Content.Headers.ContentLength = data.Length;

			var response = new ResponseMessageResult(result);
			return response;
		}
	}
}