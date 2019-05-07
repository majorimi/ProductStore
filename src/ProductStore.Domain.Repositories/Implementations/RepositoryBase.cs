using System.Threading;
using System.Threading.Tasks;
using ProductStore.Domain.Data.Context;

namespace ProductStore.Domain.Repositories.Implementations
{
	public abstract class RepositoryBase
	{
		protected readonly IProductStoreContext _dbContext;

		protected RepositoryBase(IProductStoreContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<int> SaveAsync(CancellationToken cancellationToken)
		{
			return await _dbContext.DbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
