using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductStore.Domain.Models;

namespace ProductStore.Domain.Repositories.Interfaces
{
	public interface IProductImageRepository : ISaveRepository
	{
		Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);

		Task<ProductImage> CreateAsync(int productId, byte[] data, CancellationToken cancellationToken);
	}
}