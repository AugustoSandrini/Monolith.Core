using Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Common.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string USER_PROJECTION_DATABASE_NAME = "User";

        public static IServiceCollection AddCommonServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            //services
            //    .AddAuthAuthentication();

            //services
            //    .AddTransient<IService, Service>();

            //services
            //    .ConfigureOptions<Setup>()

            services.ConfigureOptions<EnvironmentOptionsSetup>();


            return services;
        }
    }
}
