using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers;

public class SendToBmpWhenAddressUpsertConsumer(
    ISendToBmpWhenAddressUpsertHandler eventHandler) : IConsumer<UserDomainEvent.AddressUpserted>
{
    public Task Consume(ConsumeContext<UserDomainEvent.AddressUpserted> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);
}