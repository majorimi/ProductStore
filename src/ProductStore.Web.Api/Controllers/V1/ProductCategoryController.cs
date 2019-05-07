using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using ProductStore.Domain.Repositories.Interfaces;
using ProductStore.Web.Api.Infrastructure.Filters.Security;
using ProductStore.Web.Api.Infrastructure.Filters.Validation;
using ProductStore.Web.Models;
using Swashbuckle.Swagger.Annotations;

namespace ProductStore.Web.Api.Controllers.V1
{
	[SwaggerResponse(HttpStatusCode.Unauthorized, "Access denied, request is not authenticated")]
	[RoutePrefix("api/v1/ProductCategory")]
	public class ProductCategoryController : ApiController
	{
		private readonly IProductCategoryRepository _productCategoryRepository;

		public ProductCategoryController(IProductCategoryRepository productCategoryRepository)
		{
			_productCategoryRepository = productCategoryRepository;
		}

		[ApiModelValidate]
		[SwaggerResponse(HttpStatusCode.OK, "Returns all available ProductCategories in DB", typeof(IEnumerable<ProductCategoryModel>))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[HttpGet]
		public async Task<IHttpActionResult> GetAll([FromUri]PagingModel paging, CancellationToken cancellationToken)
		{
			var ret = await _productCategoryRepository.GetAllAsync(paging.PageSize, paging.PageIndex, cancellationToken);

			return Ok(Mapper.Instance.Map<IEnumerable<ProductCategoryModel>>(ret));
		}

		[SwaggerResponse(HttpStatusCode.OK, "Returns one ProductCategories by Id", typeof(ProductCategoryModel))]
		[SwaggerResponse(HttpStatusCode.NotFound, "No result was found by the given query")]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[HttpGet]
		public async Task<IHttpActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			if (id <= 0)
			{
				return BadRequest($"Parameter: {nameof(id)} is required.");
			}

			var ret = await _productCategoryRepository.GetByIdAsync(id, cancellationToken);

			if (ret == null)
			{
				return StatusCode(HttpStatusCode.NotFound);
			}

			return Ok(Mapper.Instance.Map<ProductCategoryModel>(ret));
		}

		[SwaggerResponse(HttpStatusCode.OK, "Returns all available ProductCategories by name", typeof(IEnumerable<ProductCategoryModel>))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[HttpGet]
		[Route("{name}")]
		public async Task<IHttpActionResult> GetByName(string name, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return BadRequest($"Parameter: {nameof(name)} is required.");
			}

			var ret = await _productCategoryRepository.GetByNameAsync(name, cancellationToken);

			return Ok(Mapper.Instance.Map<IEnumerable<ProductCategoryModel>>(ret));
		}

		[AdminAuthorize]
		[ApiModelValidate]
		[SwaggerResponse(HttpStatusCode.Created, "Returns with the newly created object Id", typeof(int))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[HttpPost]
		public async Task<IHttpActionResult> Post(CreateProductCategoryModel model, CancellationToken cancellationToken)
		{
			var ret = await _productCategoryRepository.CreateAsync(model.Name, cancellationToken);

			await _productCategoryRepository.SaveAsync(cancellationToken);

			return Created($"api/v1/ProductCategory/{ret.Id}", ret.Id);
		}
	}
}
