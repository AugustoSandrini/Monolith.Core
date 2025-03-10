using User.Application.UseCases.Events;
using MassTransit;
using UserEvent = User.Domain.Event;

namespace WebBff.ServiceInstallers.EventBus.Consumers.Customers
{
    public class CreateUserWhenUserDocumentAndPhoneValidatedConsumer(
        ICreateUserWhenUserDocumentAndPhoneValidatedHandler eventHandler) : IConsumer<UserEvent.UserDocumentAndPhoneValidated>
    {
        public Task Consume(ConsumeContext<UserEvent.UserDocumentAndPhoneValidated> context)
            => eventHandler.Handle(context.Message, context.CancellationToken);
    }
}
