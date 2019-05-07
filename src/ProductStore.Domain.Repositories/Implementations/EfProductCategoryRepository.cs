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
	public class EfProductCategoryRepository : RepositoryBase, IProductCategoryRepository
	{
		public EfProductCategoryRepository(IProductStoreContext dbContext) 
			: base(dbContext)
		{
		}

		public async Task<IEnumerable<ProductCategory>> GetAllAsync(int pageSize, int page, CancellationToken cancellationToken)
		{
			return await _dbContext.ProductCategories
				.OrderBy(t => t.Id)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync(cancellationToken);
		}

		public async Task<ProductCategory> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await _dbContext.ProductCategories.SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
		}

		public async Task<IEnumerable<ProductCategory>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			return await _dbContext.ProductCategories.Where(s => s.Name.Contains(name))
				.ToListAsync(cancellationToken);
		}

		public Task<ProductCategory> CreateAsync(string name, CancellationToken cancellationToken)
		{
			var cat = new ProductCategory()
			{
				Name = name,
				CreatedAtUtc = DateTime.UtcNow,
			};

			_dbContext.ProductCategories.Add(cat);

			return Task.FromResult(cat);
		}
	}
}