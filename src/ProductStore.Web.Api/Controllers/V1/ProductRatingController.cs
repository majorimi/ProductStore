using System.Net;
using System.Web.Http;
using ProductStore.Domain.Repositories.Interfaces;
using Swashbuckle.Swagger.Annotations;

namespace ProductStore.Web.Api.Controllers.V1
{
	[SwaggerResponse(HttpStatusCode.Unauthorized, "Access denied, request is not authenticated")]
	[RoutePrefix("api/v1/ProductRating")]
	public class ProductRatingController : ApiController
	{
		private readonly IProductRatingRepository _productRatingRepository;

		public ProductRatingController(IProductRatingRepository productRatingRepository)
		{
			_productRatingRepository = productRatingRepository;
		}
	}
}