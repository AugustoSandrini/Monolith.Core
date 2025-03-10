using System.ComponentModel.DataAnnotations;

namespace Common.Options
{
    public abstract record S3ClientOptions
    {
        [Required]
        public string AccessKey { get; init; }

        [Required]
        public string SecretKey { get; init; }
    }
}
