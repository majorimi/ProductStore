using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ProductStore.Web.Shared.Logging;

namespace ProductStore.Web.Api.Infrastructure.Tracer
{
	public class TraceMessageHandler : DelegatingHandler
	{
		private static readonly ILogger TraceLogger = new Logger(nameof(TraceMessageHandler));

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			TraceLogger.Info($"Request arrived Method: {request.Method}, Uri: {request.RequestUri}");

			var ret = await base.SendAsync(request, cancellationToken);

			TraceLogger.Info($"Request was served: {request.Method}, Uri: {request.RequestUri}. Response StatusCode: {ret.StatusCode}, isSuccess: {ret.IsSuccessStatusCode}, ReasonPhrase: {ret.ReasonPhrase}");

			return ret;
		}
	}
}