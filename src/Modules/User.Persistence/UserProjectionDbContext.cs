using Core.Persistence.Projection.Abstractions;

namespace User.Persistence
{
    public interface IUserProjectionDbContext : IMongoDbContext { }

    public class UserProjectionDbContext(string connectionString, string databaseName) : MongoDbContext(connectionString, databaseName), IUserProjectionDbContext;
}