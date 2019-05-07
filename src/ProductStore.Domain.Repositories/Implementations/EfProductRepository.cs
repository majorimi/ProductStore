﻿using System;
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
	public class EfProductRepository : RepositoryBase, IProductRepository
	{
		public EfProductRepository(IProductStoreContext dbContext) 
			: base(dbContext)
		{
		}

		public async Task<IEnumerable<Product>> GetAllAsync(int pageSize, int page, CancellationToken cancellationToken)
		{
			return await _dbContext.Products
				.OrderBy(t => t.Id)
				.Where(x => !x.Deleted)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync(cancellationToken);
		}

		public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			return await _dbContext.Products
				.SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
		}

		public async Task<Product> GetByCategoryIdAsync(int categoryId, CancellationToken cancellationToken)
		{
			return await _dbContext.Products
				.Include(i => i.ProductCategory)
				.Where(x => !x.Deleted)
				.SingleOrDefaultAsync(s => s.ProductCategory.Id == categoryId, cancellationToken);
		}

		public async Task<IEnumerable<Product>> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			return await _dbContext.Products.Where(s => s.Name.Contains(name) && !s.Deleted)
				.ToListAsync(cancellationToken);
		}

		public async Task<Product> CreateAsync(string name, string description, decimal price, int categoryId, CancellationToken cancellationToken)
		{
			var category = await _dbContext.ProductCategories.SingleOrDefaultAsync(x => x.Id == categoryId);
			if (category == null)
			{
				return null;
			}

			var product = new Product()
			{
				Name = name,
				Description = description,
				Price = price,
				ProductCategory = category,
				CreatedAtUtc = DateTime.UtcNow
			};

			_dbContext.Products.Add(product);

			return product;
		}
	}
}