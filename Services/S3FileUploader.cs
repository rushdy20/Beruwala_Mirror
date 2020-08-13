using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Beruwala_Mirror.Models.Admin;
using Beruwala_Mirror.Models.News;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Newtonsoft.Json.JsonConvert;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Beruwala_Mirror.Services
{
    public class S3FileUploader : IFileUploader
    {
        private readonly IConfiguration _configuration;
        private const string bucketName = "beruwalamirror";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
        private static IAmazonS3 _s3Client;

        public S3FileUploader(IConfiguration configuration)
        {
            _configuration = configuration;
            _s3Client = new AmazonS3Client(bucketRegion);
        }

        public async Task<bool> UploadFileAsync(string fileName, Stream storageStream)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentException("File name must be specified.");

            var fileTransferUtility =
                new TransferUtility(_s3Client);
            try
            {
                using (var client = new AmazonS3Client(bucketRegion))
                {
                    if (storageStream.Length > 0)
                        if (storageStream.CanSeek)
                            storageStream.Seek(0, SeekOrigin.Begin);

                    var request = new PutObjectRequest
                    {
                        AutoCloseStream = true,
                        BucketName = bucketName,
                        InputStream = storageStream,
                        Key = fileName
                    };
                    var response = await client.PutObjectAsync(request).ConfigureAwait(false);
                    return response.HttpStatusCode == HttpStatusCode.OK;
                }
            }
            catch (AmazonS3Exception ex)
            {
                return false;
                //ignore
            }
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
                        BucketName = bucketName,
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
                    BucketName = bucketName,
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

        public async Task<bool> UpdateS3ReaderCount(bool isFromFile)
        {

            var s3readCount = await GetFileFromS3(@"News/S3ReadCount.json");
            if (s3readCount == null)
            {
                // return new NewsCategories();
                return false;
            }

            var s3count = DeserializeObject<NewsCategories>(s3readCount);

            var today = DateTime.Today.ToString("dd-MM-yyyy");
             today = isFromFile? $"{today}-FromFile" : today;
            var todayCount = new CategoryModel { CategoryId = 0, Name = today };

            if (s3count.Categories.Any(s => s.Name == today))

                todayCount = s3count.Categories.First(s => s.Name == today);
            if (todayCount != null)
            {
                s3count.Categories.Remove(todayCount);
            }

            todayCount.CategoryId = todayCount.CategoryId + 1;

            s3count.Categories.Add(todayCount);
            var jsonString = JsonSerializer.Serialize(s3count);

            await SaveFileAsync(@"News/S3ReadCount.json", jsonString);

            return true;
        }

        public string ReadFile(string fullName)
        {
            var path = System.IO.Path.Combine("wwwroot", "supportFiles", fullName);
            var jsonString = File.ReadAllText(path);
           // var myJObject = JObject.Parse(myJsonString);

            return jsonString;
        }

        public string saveToFile(string fullName, string content)
        {
            var path = System.IO.Path.Combine("wwwroot", "supportFiles", fullName);
            File.WriteAllText(path, content);
            // var myJObject = JObject.Parse(myJsonString);

            return string.Empty;
        }
    }
}
