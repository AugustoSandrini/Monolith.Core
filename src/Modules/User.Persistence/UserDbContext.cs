using User.Persistence.Constants;
using Microsoft.EntityFrameworkCore;

namespace User.Persistence
{
    /// <summary>
    /// Represents the users module database context.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="UserDbContext"/> class.
    /// </remarks>
    /// <param name="options">The database context options.</param>
    public sealed class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {

        /// <inheritdoc />
        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schemas.Users);

            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        }
    }
}
