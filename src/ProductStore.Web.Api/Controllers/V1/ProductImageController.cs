using System.Net;
using System.Web.Http;
using ProductStore.Domain.Repositories.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace ProductStore.Web.Api.Controllers.V1
{
	[SwaggerResponse(HttpStatusCode.Unauthorized, "Access denied, request is not authenticated")]
	[RoutePrefix("api/v1/ProductImage")]
	public class ProductImageController : ApiController
	{
		private readonly IProductImageRepository _productImageRepository;

		public ProductImageController(IProductImageRepository productImageRepository)
		{
			_productImageRepository = productImageRepository;
		}
	}
}