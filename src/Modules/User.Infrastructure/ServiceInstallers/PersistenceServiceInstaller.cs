using Core.Application;
using Core.Application.EventStore;
using Core.Infrastructure.Configuration;
using Core.Persistence;
using Core.Persistence.EventStore;
using Core.Persistence.Extensions;
using Core.Persistence.Options;
using User.Persistence;
using User.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using User.Persistence.Projections;

namespace User.Infrastructure.ServiceInstallers
{
    /// <summary>
    /// Represents the users module persistence service installer.
    /// </summary>
    internal sealed class PersistenceServiceInstaller : IServiceInstaller
    {
        /// <inheritdoc />
        public void Install(IServiceCollection services, IConfiguration configuration) =>
            services
                .AddScoped(typeof(IUserProjection<>), typeof(UserProjection<>))
                .AddScoped(typeof(IEventStore<UserDbContext>), typeof(EventStore<UserDbContext>))
                .AddScoped(typeof(IUnitOfWork<UserDbContext>), typeof(UnitOfWork<UserDbContext>))
                .AddScoped<IUserProjectionDbContext>(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    string connectionString = configuration.GetSection("Projections").GetValue<string>("User");

                    return new UserProjectionDbContext(connectionString);
                })
                .AddDbContext<UserDbContext>((provider, builder) =>
                {
                    ConnectionStringOptions connectionString = provider.GetService<IOptions<ConnectionStringOptions>>()!.Value;

                    builder
                        .UseNpgsql(
                            connectionString: connectionString,
                            dbContextOptionsBuilder => dbContextOptionsBuilder.WithMigrationHistoryTableInSchema(Schemas.Users));
                });
    }
}
