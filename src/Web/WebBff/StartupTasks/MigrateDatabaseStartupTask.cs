using User.Persistence;
using Microsoft.EntityFrameworkCore;

namespace WebBff.StartupTasks
{
    /// <summary>
    /// Represents the startup task for migrating the database in the development environment only.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MigrateDatabaseStartupTask"/> class.
    /// </remarks>
    /// <param name="environment">The environment.</param>
    /// <param name="serviceProvider">The service provider.</param>
    internal sealed class MigrateDatabaseStartupTask(
        IServiceProvider serviceProvider) : BackgroundService
    {

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using IServiceScope scope = serviceProvider.CreateScope();

            await MigrateDatabaseAsync<UserDbContext>(scope, cancellationToken);
        }

        private static async Task MigrateDatabaseAsync<TDbContext>(IServiceScope scope, CancellationToken cancellationToken)
            where TDbContext : DbContext
        {
            TDbContext dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

            await dbContext.Database.MigrateAsync(cancellationToken);
        }
    }
}
