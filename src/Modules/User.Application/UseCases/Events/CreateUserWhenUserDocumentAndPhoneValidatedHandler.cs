using User.Domain;
using MediatR;
//using Order.Shared.Commands;
using Serilog;

namespace User.Application.UseCases.Events
{
    using Core.Application.EventBus;
    using User.Application.Extensions;
    using User.Application.Services;
    using User.Domain.Aggregates;

    public interface ICreateUserWhenUserDocumentAndPhoneValidatedHandler : IEventHandler<Event.UserDocumentAndPhoneValidated>;
    
    public class CreateUserWhenUserDocumentAndPhoneValidatedHandler(
        IUserApplicationService applicationService,
        ILogger logger,
        ISender sender) : ICreateUserWhenUserDocumentAndPhoneValidatedHandler
    {
        public async Task Handle(Event.UserDocumentAndPhoneValidated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var UserId = @event.UserId;

                if (@event.UserId == Guid.Empty)
                    UserId = await CreateUserAsync(@event.Document, @event.Phone, cancellationToken);

                //await applicationService.PublishEventAsync(new Order.Domain.Event.OrderRequested(UserId, @event.EstablishmentId, @event.RequestedAmount), cancellationToken);
            }
            catch (Exception ex) 
            {
                logger.Error(ex, ex.Message);

                throw;
            } 
        }

        private async Task<Guid> CreateUserAsync(string document, string phone, CancellationToken cancellationToken)
        {
            var User = Domain.Aggregates.User.Create(Guid.NewGuid(), document, phone.RemoveSpecialCharacters());

            await applicationService.AppendEventsAsync(User, cancellationToken);

            return User.Id;
        }

        private async Task CreateOrderAsync(Guid UserId, Guid establishmentId, decimal requestedAmount, CancellationToken cancellationToken)
        {
            //var orderCreated = await sender.Send(new CreateOrderCommand(UserId, establishmentId, requestedAmount), cancellationToken);

            //if (orderCreated.IsFailure)
            //    throw new Exception($"Falha ao incluir pedido para o cliente: {UserId} no estabelecimento: {establishmentId} com o valor requisitado: {requestedAmount}.");  
        }
    }
}
