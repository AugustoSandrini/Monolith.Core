using Microsoft.Extensions.Options;

namespace WebBff.Handlers.Options
{
    /// <summary>
    /// Represents the <see cref="BasicAuthenticationOptionsSetup"/> setup.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="BasicAuthenticationOptionsSetup"/> class.
    /// </remarks>
    /// <param name="configuration">The configuration.</param>
    public class BasicAuthenticationOptionsSetup(IConfiguration configuration) : IConfigureOptions<BasicAuthenticationOptions>
    {
        private const string ConfigurationSectionName = "BasicAuthenticationOptions";

        /// <inheritdoc />
        public void Configure(BasicAuthenticationOptions options) => configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}
