using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace User.Persistence;

internal sealed class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
{
    public UserDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseMySql(
                "Server=127.0.0.1;Port=3306;Database=eventStore;User=root;Password=!MyStrongPassword;",
                new MySqlServerVersion(new Version(8, 0, 0)),
                o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore))
            .Options;

        return new UserDbContext(options);
    }
}
