using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Beruwala_Mirror.Services
{
    public class CashManager :ICacheManager
    {
        private readonly IMemoryCache _cache;

        public CashManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void RemoveCache(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public void Set<T>(string cacheKey, T getItemCallBack)
        {
           var item = getItemCallBack;
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(24));
            _cache.Set(cacheKey, item, cacheEntryOptions);
            
        }

        public T Get<T>(string cacheKey)
        {
            return _cache.Get<T>(cacheKey);
        }
    }
}
