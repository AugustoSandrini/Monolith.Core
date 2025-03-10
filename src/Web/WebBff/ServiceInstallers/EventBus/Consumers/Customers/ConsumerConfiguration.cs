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
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<CreateUserWhenUserDocumentAndPhoneValidatedConsumer, UserEvent.UserDocumentAndPhoneValidated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<CreateVindiUserWhenProfileUpdatedConsumer, UserDomainEvent.ProfileUpdated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<DeleteUserWhenUserDeletedConsumer, UserDomainEvent.UserDeleted>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectCreditConsultationWhenCreditConsultationChangedConsumer, UserDomainEvent.CreditConsultationCreated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectCreditConsultationWhenCreditConsultationChangedConsumer, UserDomainEvent.CreditConsultationAccepted>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectCreditConsultationWhenCreditConsultationChangedConsumer, UserDomainEvent.CreditConsultationExpired>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectCreditConsultationWhenCreditConsultationChangedConsumer, UserDomainEvent.CreditConsultationRefused>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserCreated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.AddressUpserted>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserVindiExternalIdLinked>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.LastTokenSentAtUpdated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserActiveStatus>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserSaleInProgressStatus>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserDefaulterStatus>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.UserPendingDebtStatus>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectUserWhenUserChangedConsumer, UserDomainEvent.ProfileUpdated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectEmailWhenProfileUpdatedConsumer, UserDomainEvent.ProfileUpdated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectPaymentProfileWhenPaymentProfileChangedConsumer, UserDomainEvent.PaymentProfileCreated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<ProjectPhoneWhenUserCreatedConsumer, UserDomainEvent.UserCreated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<SendSmsTokenWhenCreditConsultationCreatedConsumer, UserDomainEvent.CreditConsultationCreated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<SendToBmpWhenAddressUpsertConsumer, UserDomainEvent.AddressUpserted>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<SendToBmpWhenProfileUpdatedConsumer, UserDomainEvent.ProfileUpdated>(registrationContext, MODULE_NAME);
            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<UpdateLastTokenSentAtWhenTokenSentConsumer, UserDomainEvent.UserVindiExternalIdLinked>(registrationContext, MODULE_NAME);

            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<SendEmailWhenCreditConsultationAcceptedConsumer, UserDomainEvent.CreditConsultationAccepted>(registrationContext, MODULE_NAME);

            rabbitMqBusFactoryConfigurator.ConfigureEventReceiveEndpoint<SendEmailWhenCreditAnalysisCreatedConsumer, UserDomainEvent.CreditConsultationCreated>(registrationContext, MODULE_NAME);
        }
    }
}
