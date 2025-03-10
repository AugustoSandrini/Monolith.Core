using Core.Persistence.Configurations;

namespace User.Persistence.Configurations
{
    using User.Domain.Aggregates;

    public class SnapshotConfiguration
    {
        public class UserSnapshotConfiguration : SnapshotConfiguration<User>;
    }
}
