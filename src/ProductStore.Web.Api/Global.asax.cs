using System.Web.Http;
using log4net.Config;

namespace ProductStore.Web.Api
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			XmlConfigurator.Configure();

			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}
