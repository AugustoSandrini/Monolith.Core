using Common.Options;
using Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Common.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string User_PROJECTION_DATABASE_NAME = "User";
        private const string ORDER_PROJECTION_DATABASE_NAME = "Order";

        public static IServiceCollection AddCommonServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            //services
            //    .AddIntegrationBmp()
            //    .AddIntegrationVindi()
            //    .AddCommunication()
            //    .AddAuth0Api()
            //    .AddIntegrationB2e()
            //    .AddIntegrationCaf(configuration.GetSection("Projections").GetValue<string>("Order"))
            //    .AddIntegrationClaro(configuration.GetSection("Projections").GetValue<string>("User"));

            //services
            //    .AddAuthAuthentication();

            //services
            //    .AddTransient<IS3Service, S3Service>();

            //services
            //    .ConfigureOptions<S3BucketDocumentSetup>()
            //    .ConfigureOptions<EnvironmentOptionsSetup>()
            //    .ConfigureOptions<DiscordWebHooksOptionsSetup>();

            return services;
        }
    }
}
