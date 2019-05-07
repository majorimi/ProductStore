using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using ProductStore.Web.Shared.Exceptions;
using ProductStore.Web.Shared.Logging;

namespace ProductStore.Web.Api.Infrastructure.Filters.ErrorHandling
{
	public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private static readonly Logger Logger = new Logger();

		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			if (actionExecutedContext.Exception is NotFoundException)
			{
				actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound, actionExecutedContext.Exception.Message);
			}
			else if (actionExecutedContext.Exception is InvalidInputException)
			{
				actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, actionExecutedContext.Exception.Message);
			}
			else
			{
				Logger.Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);

				actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new Error(actionExecutedContext));
			}
		}
	}
}