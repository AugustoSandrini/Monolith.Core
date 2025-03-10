using System.ComponentModel.DataAnnotations;

namespace Common.Options
{
    public sealed record S3BucketDocumentOptions : S3ClientOptions
    {
        [Required]
        public string Bucket { get; init; }
    }
}
