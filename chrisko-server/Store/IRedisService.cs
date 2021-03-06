using System;
namespace ChrisKo.Store
{
    public interface IRedisService {
        void Store(string key, object content);
        void Store(string key, object content, int duration);
        T Get<T>(string key) where T : class;
    }
}