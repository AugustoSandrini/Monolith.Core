using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Common.Options;
using Microsoft.Extensions.Options;

namespace Common.Services
{
    public class S3Service(IOptionsSnapshot<S3BucketDocumentOptions> options) : IS3Service
    {
        private readonly S3BucketDocumentOptions _options = options.Value;
        public async Task UploadFileAsync(MemoryStream memoryStream, string key, CancellationToken cancellationToken = default)
            => await GenerateTransferUtility().UploadAsync(memoryStream, _options.Bucket, key, cancellationToken);

        public async Task<GetObjectResponse> ReadFileAsync(string key, CancellationToken cancellationToken = default)
            => await new AmazonS3Client(new BasicAWSCredentials(_options.AccessKey, _options.SecretKey), Amazon.RegionEndpoint.USEast1)
                .GetObjectAsync(_options.Bucket, key, cancellationToken);

        public async Task DeleteFileAsync(string key, CancellationToken cancellationToken)
            => await new AmazonS3Client(new BasicAWSCredentials(_options.AccessKey, _options.SecretKey), Amazon.RegionEndpoint.USEast1)
            .DeleteObjectAsync(_options.Bucket, key, cancellationToken);

        #region Private Methods

        private TransferUtility GenerateTransferUtility()
            => new(new AmazonS3Client(new BasicAWSCredentials(_options.AccessKey, _options.SecretKey), Amazon.RegionEndpoint.USEast1));

        #endregion
    }
}
