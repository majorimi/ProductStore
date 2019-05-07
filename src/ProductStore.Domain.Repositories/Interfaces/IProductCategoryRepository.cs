using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductStore.Domain.Models;

namespace ProductStore.Domain.Repositories.Interfaces
{
	public interface IProductCategoryRepository : ISaveRepository
	{
		Task<IEnumerable<ProductCategory>> GetAllAsync(int pageSize, int page, CancellationToken cancellationToken);

		Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken);

		Task<IEnumerable<ProductCategory>> GetByNameAsync(string name, CancellationToken cancellationToken);

		Task<ProductCategory> CreateAsync(string name, CancellationToken cancellationToken);
	}
}