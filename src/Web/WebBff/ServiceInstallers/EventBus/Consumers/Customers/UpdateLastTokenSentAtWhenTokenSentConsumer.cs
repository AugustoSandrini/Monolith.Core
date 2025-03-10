using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers;

public class UpdateLastTokenSentAtWhenTokenSentConsumer(
    IUpdateLastTokenSentAtWhenTokenSentHandler eventHandler) : IConsumer<UserDomainEvent.UserVindiExternalIdLinked>
{
    public Task Consume(ConsumeContext<UserDomainEvent.UserVindiExternalIdLinked> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);
}