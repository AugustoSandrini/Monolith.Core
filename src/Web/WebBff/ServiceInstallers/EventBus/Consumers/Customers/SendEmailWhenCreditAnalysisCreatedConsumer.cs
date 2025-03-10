using User.Application.UseCases.Events;
using User.Domain;
using MassTransit;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class SendEmailWhenCreditAnalysisCreatedConsumer(ISendEmailWhenCreditConsultationCreatedHandler eventHandler) : IConsumer<DomainEvent.CreditConsultationCreated>
    {
        public Task Consume(ConsumeContext<DomainEvent.CreditConsultationCreated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
