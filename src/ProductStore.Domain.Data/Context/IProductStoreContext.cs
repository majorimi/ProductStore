using System.Data.Entity;
using ProductStore.Domain.Models;

namespace ProductStore.Domain.Data.Context
{
	public interface IProductStoreContext
	{
		IDbSet<ProductCategory> ProductCategories { get; }
		IDbSet<Product> Products { get; }
		IDbSet<ProductImage> ProductImages { get; }
		IDbSet<ProductRating> ProductRatings { get; }

		DbContext DbContext { get; }
	}
}