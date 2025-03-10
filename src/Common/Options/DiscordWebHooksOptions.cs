using System.ComponentModel.DataAnnotations;

namespace Common.Options
{
    public record DiscordWebHooksOptions
    {
        [Required, Url]
        public string EstablishmentWebHookUrl { get; set; }
    }
}
