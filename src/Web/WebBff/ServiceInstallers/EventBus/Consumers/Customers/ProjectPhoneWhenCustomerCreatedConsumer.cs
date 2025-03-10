using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class ProjectPhoneWhenUserCreatedConsumer(
        IProjectPhoneWhenUserCreatedHandler eventHandler) : IConsumer<UserDomainEvent.UserCreated>
    {
        public Task Consume(ConsumeContext<UserDomainEvent.UserCreated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
