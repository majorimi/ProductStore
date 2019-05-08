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
	[RoutePrefix("api/v1/Product")]
	public class ProductController : ApiController
	{
		private readonly IProductRepository _productRepository;

		public ProductController(IProductRepository productRepository)
		{
			_productRepository = productRepository;
		}

		[ApiModelValidate]
		[SwaggerResponse(HttpStatusCode.OK, "Returns all available Products in DB", typeof(IEnumerable<ProductModel>))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[HttpGet]
		public async Task<IHttpActionResult> GetAll([FromUri]PagingModel paging, CancellationToken cancellationToken)
		{
			var ret = await _productRepository.GetAllAsync(paging.PageSize, paging.PageIndex, cancellationToken);

			return Ok(Mapper.Instance.Map<IEnumerable<ProductModel>>(ret));
		}

		[SwaggerResponse(HttpStatusCode.OK, "Returns one Product by Id", typeof(ProductModel))]
		[SwaggerResponse(HttpStatusCode.NotFound, "No result was found by the given query")]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[HttpGet]
		public async Task<IHttpActionResult> GetById(int id, CancellationToken cancellationToken)
		{
			if (id <= 0)
			{
				return BadRequest($"Parameter: {nameof(id)} is required.");
			}

			var ret = await _productRepository.GetByIdAsync(id, cancellationToken);

			if (ret == null)
			{
				return StatusCode(HttpStatusCode.NotFound);
			}

			return Ok(Mapper.Instance.Map<ProductModel>(ret));
		}

		[SwaggerResponse(HttpStatusCode.OK, "Returns all available ProductCategories by name", typeof(IEnumerable<ProductModel>))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[HttpGet]
		[Route("{name}")]
		public async Task<IHttpActionResult> GetByName(string name, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				return BadRequest($"Parameter: {nameof(name)} is required.");
			}

			var ret = await _productRepository.GetByNameAsync(name, cancellationToken);

			return Ok(Mapper.Instance.Map<IEnumerable<ProductModel>>(ret));
		}

		[AdminAuthorize]
		[ApiModelValidate]
		[SwaggerResponse(HttpStatusCode.Created, "Returns with the newly created object Id", typeof(int))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[SwaggerResponse(HttpStatusCode.Forbidden, "Access denied, request is not authorized")]
		[HttpPost]
		public async Task<IHttpActionResult> Post(CreateProductModel model, CancellationToken cancellationToken)
		{
			var ret = await _productRepository.CreateAsync(model.Name, model.Description, model.Price, model.CategoryId, cancellationToken);

			await _productRepository.SaveAsync(cancellationToken);

			return Created($"api/v1/ProductCategory/{ret.Id}", ret.Id);
		}

		[AdminAuthorize]
		[ApiModelValidate]
		[SwaggerResponse(HttpStatusCode.NoContent, "No content")]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[SwaggerResponse(HttpStatusCode.Forbidden, "Access denied, request is not authorized")]
		[HttpPut]
		public async Task<IHttpActionResult> Put(UpdateProductModel model, CancellationToken cancellationToken)
		{
			var ret  = await _productRepository.UpdateAsync(model.Id, model.Name, model.Description, model.Price, model.CategoryId, cancellationToken);
			if (!ret)
			{
				return BadRequest();
			}
			
			await _productRepository.SaveAsync(cancellationToken);

			return StatusCode(HttpStatusCode.NoContent);
		}

		[AdminAuthorize]
		[SwaggerResponse(HttpStatusCode.NoContent, "No content")]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request, missing or incorrect input", typeof(string))]
		[SwaggerResponse(HttpStatusCode.Forbidden, "Access denied, request is not authorized")]
		[HttpDelete]
		public async Task<IHttpActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			if (id <= 0)
			{
				return BadRequest($"Parameter: {nameof(id)} is required.");
			}

			var ret = await _productRepository.DeleteAsync(id, cancellationToken);
			if (!ret)
			{
				return BadRequest();
			}

			await _productRepository.SaveAsync(cancellationToken);

			return StatusCode(HttpStatusCode.NoContent);
		}
	}
}
