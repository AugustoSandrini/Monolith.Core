using Common.HealthChecks.Memory;
using CorrelationId.DependencyInjection;
using User.Persistence;


namespace WebBff.Extensions;

internal static class ServiceCollectionExtensions
{
    internal const string LoggingScopeKey = "CorrelationId";

    internal static IServiceCollection AddCorrelationId(this IServiceCollection services)
        => services.AddDefaultCorrelationId(options =>
        {
            options.RequestHeader =
                options.ResponseHeader =
                    options.LoggingScopeKey = LoggingScopeKey;

            options.UpdateTraceIdentifier =
                options.AddToLoggingScope = true;
        });

    internal static void AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        => services.AddHealthChecks()
            .AddMemory(
                name: "Memory",
                tags: ["memory"])
            .AddNpgSql(
                connectionString: configuration.GetConnectionString("Default"),
                name: "EventStore",
                tags: ["database", "postgress"])
            .AddMongoDb(clientFactory: provider =>
                {
                    var context = provider.GetRequiredService<IUserProjectionDbContext>();
                    return context.MongoClient;
                },
                databaseNameFactory: provider =>
                {
                    var context = provider.GetRequiredService<IUserProjectionDbContext>();
                    return context.DatabaseName;
                },
                name: "User",
                tags: ["database", "mongo"]);
}
