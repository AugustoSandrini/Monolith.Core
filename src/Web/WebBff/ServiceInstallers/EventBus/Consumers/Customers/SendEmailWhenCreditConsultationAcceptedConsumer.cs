using User.Application.UseCases.Events;
using User.Domain;
using MassTransit;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class SendEmailWhenCreditConsultationAcceptedConsumer(ISendEmailWhenCreditConsultationAcceptedHandler eventHandler) : IConsumer<DomainEvent.CreditConsultationAccepted>
    {
        public Task Consume(ConsumeContext<DomainEvent.CreditConsultationAccepted> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
