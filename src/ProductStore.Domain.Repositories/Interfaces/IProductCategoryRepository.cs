using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ProductStore.Domain.Models;

namespace ProductStore.Domain.Repositories.Interfaces
{
	public interface ISaveRepository
	{
		Task<int> SaveAsync(CancellationToken cancellationToken);
	}

	public interface IProductCategoryRepository : ISaveRepository
	{
		Task<IEnumerable<ProductCategory>> GetAllAsync(int pageSize, int page, CancellationToken cancellationToken);

		Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken);

		Task<IEnumerable<ProductCategory>> GetByNameAsync(string name, CancellationToken cancellationToken);

		Task<ProductCategory> CreateAsync(string name, CancellationToken cancellationToken);
	}

	public interface IProductRepository : ISaveRepository
	{
		Task<IEnumerable<Product>> GetAllAsync(int pageSize, int page, CancellationToken cancellationToken);

		Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);

		Task<Product> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken);

		Task<IEnumerable<Product>> GetByNameAsync(string name, CancellationToken cancellationToken);

		Task<Product> CreateAsync(string name, string description, decimal price, int categoryId, CancellationToken cancellationToken);
	}

	public interface IProductRatingRepository : ISaveRepository
	{
		Task<double> GetByProductIdAsync(int productId, CancellationToken cancellationToken);

		Task<ProductRating> CreateAsync(int productId, double rating, int userId, CancellationToken cancellationToken);
	}

	public interface IProductImageRepository : ISaveRepository
	{
		Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId, CancellationToken cancellationToken);

		Task<ProductImage> CreateAsync(int productId, byte[] data, CancellationToken cancellationToken);
	}
}