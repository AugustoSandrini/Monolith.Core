using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;


namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class CreateVindiUserWhenProfileUpdatedConsumer(
        ICreateVindiUserWhenProfileUpdatedHandler eventHandler) : IConsumer<UserDomainEvent.ProfileUpdated>
    {
        public Task Consume(ConsumeContext<UserDomainEvent.ProfileUpdated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
