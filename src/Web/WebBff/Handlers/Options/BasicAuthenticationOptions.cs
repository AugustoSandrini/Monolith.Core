using System.ComponentModel.DataAnnotations;

namespace WebBff.Handlers.Options
{
    public record BasicAuthenticationOptions
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
