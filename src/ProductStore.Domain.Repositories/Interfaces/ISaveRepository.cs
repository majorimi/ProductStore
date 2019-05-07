using System.Threading;
using System.Threading.Tasks;

namespace ProductStore.Domain.Repositories.Interfaces
{
	public interface ISaveRepository
	{
		Task<int> SaveAsync(CancellationToken cancellationToken);
	}
}