using System.Net.Http;
using System.Web.Http.Filters;

namespace ProductStore.Web.Api.Infrastructure.Filters.ErrorHandling
{
	public class Error
	{
		public string Message { get; }

		public Error(HttpActionExecutedContext actionExecutedContext)
		{
			Message = actionExecutedContext.Request.IsLocal()
				? actionExecutedContext.Exception.ToString()
				: "Unhanded exception occurred. Please try again.";
		}
	}
}