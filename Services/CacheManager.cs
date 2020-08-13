using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Beruwala_Mirror.Models.Admin;
using Beruwala_Mirror.Models.News;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using static Newtonsoft.Json.JsonConvert;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Beruwala_Mirror.Services
{
    public class CacheManager :ICacheManager
    {
        private readonly IMemoryCache _cache;
        private readonly IFileUploader _fileUploader;
        private const string BucketName = "beruwalamirror";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
        private static IAmazonS3 _s3Client;

        public CacheManager(IMemoryCache cache, IFileUploader fileUploader)
        {
            _cache = cache;
            _fileUploader = fileUploader;
            _s3Client = new AmazonS3Client(bucketRegion);
        }
        public void RemoveCache(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public void Set<T>(string cacheKey, T getItemCallBack)
        {
           var item = getItemCallBack;
           var cacheEntryOptions = new MemoryCacheEntryOptions();
           cacheEntryOptions.AbsoluteExpiration = DateTimeOffset.Now.AddDays(15);
           cacheEntryOptions.Priority = CacheItemPriority.High;
           cacheEntryOptions.RegisterPostEvictionCallback(CachedCallBack, this);
            _cache.Set(cacheKey, item, cacheEntryOptions);
            
        }

        private void CachedCallBack(object key, object value, EvictionReason reason, object state)
        {
            var s3readCount =  GetFileFromS3(@"News/S3ReadCount.json").Result;
            if (s3readCount == null)
            {
                // return new NewsCategories();
               
            }

            var s3count = DeserializeObject<NewsCategories>(s3readCount);


          //  var today = DateTime.Today.ToString("dd-MM-yyyy");
          //  var todaycount = new CategoryModel { CategoryId = 0, Name = $"{today}-CallBack" };

          //  if (s3count.Categories.Any(s => s.Name == $"{today}-CallBack"))

          //      todaycount = s3count.Categories.First(s => s.Name == $"{today}-CallBack");
          //  if (todaycount != null)
          //  {
          //      s3count.Categories.Remove(todaycount);
          //  }

          //  todaycount.CategoryId = todaycount.CategoryId + 1;

          //  s3count.Categories.Add(todaycount);
          //  var jsonString = JsonSerializer.Serialize(s3count);

          //var update =  SaveFileAsync(@"News/S3ReadCount.json", jsonString).Result;

            
        }

        public T Get<T>(string cacheKey)
        {
            return _cache.Get<T>(cacheKey);
        }

        public void SetNewsItem(NewsModel newsModel)
        {
            var newsFromCache = _cache.Get<List<NewsModel>>("NewsItems");

            if (newsFromCache.Any(m => m.Id == newsModel.Id))
            {
                newsFromCache.Remove(newsFromCache.First(n => n.Id == newsModel.Id));
            }
            newsFromCache.Add(newsModel);
            newsFromCache = newsFromCache.OrderByDescending(n => n.DisplayDate).ToList();
            _cache.Set<List<NewsModel>>("NewsItems", newsFromCache);
        }

        public async Task<bool> SaveFileAsync(string fileName, string storageStream)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("File name must be specified.");

            try
            {

                using (var client = new AmazonS3Client(bucketRegion))
                {

                    var request = new PutObjectRequest
                    {
                        AutoCloseStream = true,
                        BucketName = BucketName,
                        ContentBody = storageStream,
                        Key = fileName
                    };
                    var response = await client.PutObjectAsync(request).ConfigureAwait(false);
                    return response.HttpStatusCode == HttpStatusCode.OK;
                }
            }
            catch (AmazonS3Exception ex)
            {
                //ignore
                return false;
            }
        }

        public async Task<string> GetFileFromS3(string filename)
        {
            try
            {

                var requestObject = new GetObjectRequest
                {
                    BucketName = BucketName,
                    Key = filename
                };

                using (var response = await _s3Client.GetObjectAsync(requestObject))
                using (var responseStream = response.ResponseStream)
                using (var reader = new StreamReader(responseStream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (AmazonS3Exception ex)
            {
                //ignore
            }

            return string.Empty;
        }
    }
}
