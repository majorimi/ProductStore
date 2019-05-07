using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProductStore.Domain.Data.Context;
using ProductStore.Domain.Models;
using ProductStore.Domain.Repositories.Interfaces;

namespace ProductStore.Domain.Repositories.Implementations
{
	public class EfProductRatingRepository : RepositoryBase, IProductRatingRepository
	{
		public EfProductRatingRepository(IProductStoreContext dbContext) 
			: base(dbContext)
		{
		}

		public async Task<double> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
		{
			return await _dbContext.ProductRatings
				.Include(i => i.Product)
				.Where(s => s.Product.Id == productId)
				.AverageAsync(s => s.Rating, cancellationToken);
		}

		public async Task<ProductRating> CreateAsync(int productId, double rating, int userId, CancellationToken cancellationToken)
		{
			var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);
			if (product == null)
			{
				return null;
			}

			var rate = new ProductRating()
			{
				Product = product,
				Rating = rating,
				CustomerId = userId,
				CreatedAtUtc = DateTime.UtcNow
			};

			_dbContext.ProductRatings.Add(rate);

			return rate;
		}
	}
}