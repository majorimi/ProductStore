using System;
using System.Runtime.Caching;

namespace ProductStore.Web.Shared.Caching
{
	public class CacheService : ICacheService
	{
		private static readonly MemoryCache MemoryCache = new MemoryCache("ApiCache");

		public void CacheItem(string key, object item, TimeSpan absoluteTime)
		{
			var policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteTime) };
			GetCache().Set(key, item, policy);
		}

		public ObjectCache GetCache()
		{
			return MemoryCache;
		}
	}
}