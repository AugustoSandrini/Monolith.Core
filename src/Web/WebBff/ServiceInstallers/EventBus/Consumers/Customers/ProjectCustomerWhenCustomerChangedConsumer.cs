using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class ProjectUserWhenUserChangedConsumer(
        IProjectUserWhenUserChangedHandler eventHandler) :
        IConsumer<UserDomainEvent.UserCreated>,
        IConsumer<UserDomainEvent.AddressUpserted>,
        IConsumer<UserDomainEvent.UserVindiExternalIdLinked>,
        IConsumer<UserDomainEvent.LastTokenSentAtUpdated>,
        IConsumer<UserDomainEvent.UserActiveStatus>,
        IConsumer<UserDomainEvent.UserSaleInProgressStatus>,
        IConsumer<UserDomainEvent.UserDefaulterStatus>,
        IConsumer<UserDomainEvent.UserPendingDebtStatus>,
        IConsumer<UserDomainEvent.ProfileUpdated>
    {
        public Task Consume(ConsumeContext<UserDomainEvent.UserCreated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.AddressUpserted> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.UserVindiExternalIdLinked> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.LastTokenSentAtUpdated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.UserActiveStatus> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.UserSaleInProgressStatus> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.UserDefaulterStatus> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
        public Task Consume(ConsumeContext<UserDomainEvent.UserPendingDebtStatus> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.ProfileUpdated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
