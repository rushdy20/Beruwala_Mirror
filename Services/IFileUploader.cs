using System.IO;
using System.Threading.Tasks;

namespace Beruwala_Mirror.Services
{
    public interface IFileUploader
    {
        Task<bool> UploadFileAsync(string fileName, Stream storageStream);
        Task<bool> SaveFileAsync(string fileName, string storageStream);

        Task<string> GetFileFromS3(string filename);

       Task<bool> UpdateS3ReaderCount(bool isFromFile);

       string ReadFile(string fullName);
       string saveToFile(string fullName, string content);

    }
}