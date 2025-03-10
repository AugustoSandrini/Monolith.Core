using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Common.Options
{
    internal class DiscordWebHooksOptionsSetup(IConfiguration configuration) : IConfigureOptions<DiscordWebHooksOptions>
    {
        private const string ConfigurationSectionName = "DiscordWebHooksOptions";

        public void Configure(DiscordWebHooksOptions options)
        {
            throw new NotImplementedException();
        }

        //public void Configure(DiscordWebHooksOptions options) => configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
