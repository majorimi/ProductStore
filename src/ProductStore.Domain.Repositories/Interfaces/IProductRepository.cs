using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductStore.Domain.Models;

namespace ProductStore.Domain.Repositories.Interfaces
{
	public interface IProductRepository : ISaveRepository
	{
		Task<IEnumerable<Product>> GetAllAsync(int pageSize, int page, CancellationToken cancellationToken);

		Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);

		Task<Product> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken);

		Task<IEnumerable<Product>> GetByNameAsync(string name, CancellationToken cancellationToken);

		Task<bool> UpdateAsync(int id, string name, string description, decimal price, int categoryId, CancellationToken cancellationToken);

		Task<Product> CreateAsync(string name, string description, decimal price, int categoryId, CancellationToken cancellationToken);

		Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
	}
}