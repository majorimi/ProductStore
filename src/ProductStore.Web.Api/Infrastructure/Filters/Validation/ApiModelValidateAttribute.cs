using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProductStore.Web.Api.Infrastructure.Filters.Validation
{
	[AttributeUsage(AttributeTargets.Method|AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class ApiModelValidateAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			if (actionContext.ModelState.IsValid)
			{
				return;
			}

			var exceptions = actionContext.ModelState.Values.SelectMany(m => m.Errors)
				.Select(e => e.Exception?.ToString())
				.Where(x => x != null);
			var errors = actionContext.ModelState.Values.SelectMany(m => m.Errors)
				.Select(e => e.ErrorMessage)
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.ToList();

			errors.AddRange(exceptions);
			var msg = errors.Any()
				? string.Join(", ", errors)
				: "Model is invalid";

			actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = msg });
		}
	}
}