using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProductStore.Domain.Data.Context;
using ProductStore.Domain.Models;
using ProductStore.Domain.Repositories.Interfaces;

namespace ProductStore.Domain.Repositories.Implementations
{
	public class EfProductImageRepository : RepositoryBase, IProductImageRepository
	{
		public EfProductImageRepository(IProductStoreContext dbContext) : base(dbContext)
		{
		}

		public async Task<IEnumerable<ProductImage>> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
		{
			return await _dbContext.ProductImages
				.Include(i => i.Product)
				.Where(s => s.Product.Id == productId)
				.ToListAsync(cancellationToken);
		}

		public async Task<ProductImage> CreateAsync(int productId, byte[] data, CancellationToken cancellationToken)
		{
			var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);
			if (product == null)
			{
				return null;
			}

			var image = new ProductImage()
			{
				Product = product,
				Image = data,
				CreatedAtUtc = DateTime.UtcNow
			};

			_dbContext.ProductImages.Add(image);

			return image;
		}
	}
}