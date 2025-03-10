using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Common.Options
{
    internal sealed class S3BucketDocumentSetup(IConfiguration configuration) : IConfigureOptions<S3BucketDocumentOptions>
    {
        private const string ConfigurationSectionName = "S3BucketDocumentOptions";

        public void Configure(S3BucketDocumentOptions options)
        {
            throw new NotImplementedException();
        }

        //public void Configure(S3BucketDocumentOptions options) => configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
