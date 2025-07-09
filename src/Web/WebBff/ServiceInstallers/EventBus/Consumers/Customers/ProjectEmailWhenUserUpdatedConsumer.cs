using MassTransit;
using User.Application.UseCases.Events;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class ProjectEmailWhenUserUpdatedConsumer(
        IProjectEmailWhenUserUpdatedHandler eventHandler) : IConsumer<UserDomainEvent.UserUpdated>
    {
        public Task Consume(ConsumeContext<UserDomainEvent.UserUpdated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
