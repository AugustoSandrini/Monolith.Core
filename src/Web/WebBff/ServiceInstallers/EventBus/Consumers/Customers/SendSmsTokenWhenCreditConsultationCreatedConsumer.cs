using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers;

public class SendSmsTokenWhenCreditConsultationCreatedConsumer(
        ISendSmsTokenWhenCreditConsultationCreatedHandler eventHandler) : IConsumer<UserDomainEvent.CreditConsultationCreated>
{
    public Task Consume(ConsumeContext<UserDomainEvent.CreditConsultationCreated> context)
        => eventHandler.Handle(context.Message, context.CancellationToken);
}