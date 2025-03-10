using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class DeleteUserWhenUserDeletedConsumer(
        IDeleteUserWhenUserDeletedHandler eventHandler) : IConsumer<UserDomainEvent.UserDeleted>
    {
        public Task Consume(ConsumeContext<UserDomainEvent.UserDeleted> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
