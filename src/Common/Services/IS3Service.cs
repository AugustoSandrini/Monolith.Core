using Amazon.S3.Model;

namespace Common.Services
{
    public interface IS3Service
    {
        Task UploadFileAsync(MemoryStream memoryStream, string key, CancellationToken cancellationToken = default);
        Task<GetObjectResponse> ReadFileAsync(string key, CancellationToken cancellationToken = default);
        Task DeleteFileAsync(string key, CancellationToken cancellationToken = default);
    }
}
