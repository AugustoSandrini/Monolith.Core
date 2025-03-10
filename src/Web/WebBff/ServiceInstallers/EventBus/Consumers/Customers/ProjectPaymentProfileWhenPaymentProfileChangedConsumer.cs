using User.Application.UseCases.Events;
using MassTransit;
using UserDomainEvent = User.Domain.DomainEvent;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class ProjectPaymentProfileWhenPaymentProfileChangedConsumer(
        IProjectPaymentProfileWhenPaymentProfileChangedHandler eventHandler) :
        IConsumer<UserDomainEvent.PaymentProfileCreated>
    {
        public Task Consume(ConsumeContext<UserDomainEvent.PaymentProfileCreated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
