namespace User.Persistence.Configurations
{
    using Core.Persistence.Configurations;
    using User.Domain.Aggregates;

    public class StoreEventConfiguration
    {
        public class UserStoreEventConfiguration : StoreEventConfiguration<User>;
    }
}
