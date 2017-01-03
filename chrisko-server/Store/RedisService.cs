using System;
// using System.Text;
// using Microsoft.Extensions.Caching.Distributed;
// using Newtonsoft.Json;

namespace ChrisKo.Store
{
    public class RedisService : IRedisService
    {
        // protected IDistributedCache Cache;
        // private static int DefaultCacheDuration => 60;
        // public RedisService(IDistributedCache cache) {
        //     Cache = cache;
        // }
        // public T Get<T>(string key) where T : class
        // {
        //     var fromCache = Cache.Get(key);
        //     if (fromCache == null)
        //     {
        //         return default(T);
        //     }

        //     var str = Encoding.UTF8.GetString(fromCache);
        //     if (typeof(T) == typeof(string))
        //     {
        //         return str as T;
        //     }

        //     return JsonConvert.DeserializeObject<T>(str);
        // }

        // public void Store(string key, object content)
        // {
        //     throw new NotImplementedException();
        // }

        // public void Store(string key, object content, int duration)
        // {
        //     throw new NotImplementedException();
        // }
        public T Get<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public void Store(string key, object content)
        {
            throw new NotImplementedException();
        }

        public void Store(string key, object content, int duration)
        {
            throw new NotImplementedException();
        }
    }
}