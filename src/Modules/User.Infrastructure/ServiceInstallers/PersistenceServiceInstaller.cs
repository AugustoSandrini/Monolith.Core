using User.Persistence;
using User.Persistence.Projections;
using Core.Application;
using Core.Application.EventStore;
using Core.Infrastructure.Configuration;
using Core.Persistence;
using Core.Persistence.EventStore;
using Core.Persistence.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace User.Infrastructure.ServiceInstallers
{
    /// <summary>
    /// Represents the users module persistence service installer.
    /// </summary>
    internal sealed class PersistenceServiceInstaller : IServiceInstaller
    {
        public const string DATABASE_NAME = "User";

        /// <inheritdoc />
        public void Install(IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped(typeof(IUserProjection<>), typeof(UserProjection<>))
                .AddScoped(typeof(IEventStore<UserDbContext>), typeof(EventStore<UserDbContext>))
                .AddScoped(typeof(IUnitOfWork<UserDbContext>), typeof(UnitOfWork<UserDbContext>))
                .AddScoped<IUserProjectionDbContext>(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    string connectionString = configuration.GetConnectionString("Projection");

                    return new UserProjectionDbContext(connectionString, DATABASE_NAME);
                })
                .AddDbContext<UserDbContext>((provider, builder) =>
                {
                    ConnectionStringOptions connectionString = provider.GetService<IOptions<ConnectionStringOptions>>()!.Value;

                    builder.UseMySql(
                            connectionString: connectionString,
                            ServerVersion.AutoDetect(connectionString));
                });
    }
}
