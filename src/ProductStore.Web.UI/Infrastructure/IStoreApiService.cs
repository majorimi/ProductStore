using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductStore.Web.Models;

namespace ProductStore.Web.UI.Infrastructure
{
	public interface IStoreApiService
	{
		Task<IEnumerable<ProductCategoryModel>> GetProductCategories(PagingModel paging, CancellationToken cancellationToken);
		Task<IEnumerable<ProductModel>> GetProducts(PagingModel paging, CancellationToken cancellationToken);
		Task<int> CreateProductAsync(CreateProductModel model, CancellationToken cancellationToken);
		Task<bool> UpdateProductAsync(UpdateProductModel model, CancellationToken cancellationToken);
	}
}