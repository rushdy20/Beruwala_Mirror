using System;
using Beruwala_Mirror.Models.News;

namespace Beruwala_Mirror.Services
{
    public interface ICacheManager
    {
        void RemoveCache(string cacheKey);
        void Set<T>(string cacheKey, T getItemCallBack);
        T Get<T>(string cacheKey);
        void SetNewsItem(NewsModel NewsModel);

    }
}
