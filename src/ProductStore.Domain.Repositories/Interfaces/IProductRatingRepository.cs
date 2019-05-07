using System.Threading;
using System.Threading.Tasks;
using ProductStore.Domain.Models;

namespace ProductStore.Domain.Repositories.Interfaces
{
	public interface IProductRatingRepository : ISaveRepository
	{
		Task<double> GetByProductIdAsync(int productId, CancellationToken cancellationToken);

		Task<ProductRating> CreateAsync(int productId, double rating, int userId, CancellationToken cancellationToken);
	}
}