using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class ProjectUserWhenUserChangedConsumer(
        IProjectUserWhenUserChangedHandler eventHandler) :
        IConsumer<UserDomainEvent.UserCreated>,
        IConsumer<UserDomainEvent.AddressUpserted>,
        IConsumer<UserDomainEvent.UserActiveStatus>,
        IConsumer<UserDomainEvent.UserDefaulterStatus>,
        IConsumer<UserDomainEvent.UserUpdated>
    {
        public Task Consume(ConsumeContext<UserDomainEvent.UserCreated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.AddressUpserted> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
        public Task Consume(ConsumeContext<UserDomainEvent.UserActiveStatus> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.UserDefaulterStatus> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.UserUpdated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
