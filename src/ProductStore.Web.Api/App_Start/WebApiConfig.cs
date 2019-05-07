using System.Web.Http;
using Newtonsoft.Json.Serialization;
using ProductStore.Web.Api.Infrastructure.Filters.ErrorHandling;
using ProductStore.Web.Api.Infrastructure.Security;
using ProductStore.Web.Api.Infrastructure.Security.Client;
using ProductStore.Web.Api.Infrastructure.Tracer;
using ProductStore.Web.Shared.Logging;

namespace ProductStore.Web.Api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			//Logger
			LoggerContext.Current = new DefaultLogger();
			config.MessageHandlers.Add(new TraceMessageHandler());

			// Web API configuration and services
			config.EnableCors();

			//Authentication
			config.MessageHandlers.Add(new BearerAuthenticationMessageHandler());
			ClientContext.Current = new ApiClientContext();

			//Authorization
			//config.Filters.Add(new AuthorizeAttribute()); //TODO: enable Auth

			// Use camel case for JSON data.
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			//Object mapping
			MapperConfig.Initialize();

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApiV1",
				routeTemplate: "api/{namespace}/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			//Filters
			config.Filters.Add(new ApiExceptionFilterAttribute());
		}
	}
}