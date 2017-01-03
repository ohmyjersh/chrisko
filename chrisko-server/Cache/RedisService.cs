using System;
using System.Text;
// using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
// using Newtonsoft.Json;

namespace ChrisKo.Cache
{
    public class RedisService : IRedisService
    {
        protected IDistributedCache _cache;
        private static int DefaultCacheDuration => 60;
        public RedisService(IDistributedCache cache) {
            _cache = cache;
        }
        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            var item = _cache.Get(cacheKey);
            if (item == null)
            {
                T storedItem = getItemCallback();
                var itemStr = JsonConvert.SerializeObject(storedItem);
                _cache.Set(cacheKey, Encoding.UTF8.GetBytes(itemStr), new DistributedCacheEntryOptions(){AbsoluteExpiration = DateTime.Now.AddDays(5)});
                return storedItem;
            }
             var str = Encoding.UTF8.GetString(item);
             return JsonConvert.DeserializeObject<T>(str);
        }
    }
}