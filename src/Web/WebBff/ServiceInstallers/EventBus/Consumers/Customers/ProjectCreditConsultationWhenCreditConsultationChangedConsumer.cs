using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class ProjectCreditConsultationWhenCreditConsultationChangedConsumer(
        IProjectCreditConsultationWhenCreditConsultationChangedHandler eventHandler) :
        IConsumer<UserDomainEvent.CreditConsultationCreated>,
        IConsumer<UserDomainEvent.CreditConsultationAccepted>,
        IConsumer<UserDomainEvent.CreditConsultationExpired>,
        IConsumer<UserDomainEvent.CreditConsultationRefused>
    {
        public Task Consume(ConsumeContext<UserDomainEvent.CreditConsultationCreated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.CreditConsultationAccepted> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.CreditConsultationExpired> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDomainEvent.CreditConsultationRefused> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
