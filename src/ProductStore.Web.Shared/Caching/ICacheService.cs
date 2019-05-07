using System;
using System.Runtime.Caching;

namespace ProductStore.Web.Shared.Caching
{
	public interface ICacheService
	{
		ObjectCache GetCache();

		void CacheItem(string key, object item, TimeSpan absoluteTime);
	}
}