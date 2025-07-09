using Core.Infrastructure.EventBus;
using MassTransit;
using UserEvent = User.Domain.Event;
using UserDomainEvent = User.Domain.DomainEvent;
using WebBff.ServiceInstallers.EventBus.Extensions;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    /// <summary>
    /// Represents the consumer configuration for the notifications module.
    /// </summary>
    internal sealed class ConsumerConfiguration : IEventReceiveEndpointConfiguration
    {
        private const string MODULE_NAME = "User";

        public void AddEventReceiveEndpoints(IRabbitMqBusFactoryConfigurator rabbitMqBusFactoryConfigurator, IRegistrationContext registrationContext)
        {
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<DeleteUserWhenUserDeletedConsumer, UserDomainEvent.UserDeleted>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserCreated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.AddressUpserted>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserActiveStatus>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserDefaulterStatus>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserUpdated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectEmailWhenUserUpdatedConsumer, UserDomainEvent.UserUpdated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectPhoneWhenUserCreatedConsumer, UserDomainEvent.UserCreated>(registrationContext, MODULE_NAME);
        }
    }
}
