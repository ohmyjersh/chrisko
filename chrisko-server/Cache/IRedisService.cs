using System;
namespace ChrisKo.Cache
{
    public interface IRedisService {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
    }
}