using User.Domain;
using Serilog;
using Core.Application.EventBus;
using User.Domain.Exceptions;

namespace User.Application.UseCases.Events
{
    using User.Application.Services;
    using User.Domain.Aggregates;

    public interface ISendToBmpWhenAddressUpsertHandler : IEventHandler<DomainEvent.AddressUpserted>;

    public class SendToBmpWhenAddressUpsertHandler(
        //IBmpClient bmpClient,
        IUserApplicationService applicationService,
        ILogger logger) : ISendToBmpWhenAddressUpsertHandler
    {

        public async Task Handle(DomainEvent.AddressUpserted @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var UserResult = await applicationService.LoadAggregateAsync<User>(@event.UserId, cancellationToken);

                if (UserResult.IsFailure)
                    throw new UserNotFoundException(@event.UserId);

                var User = UserResult.Value;

                var address = @event.Address;

                //var request = new UpsertPersonAddressRequest()
                //{
                //    Address = new()
                //    {
                //        ZipCode = address?.ZipCode,
                //        Street = address?.Street,
                //        Number = address?.Number?.ToString(),
                //        District = address?.District,
                //        Complement = address?.Complement,
                //        City = address?.City,
                //        State = address?.State
                //    },
                //    Parameters = new()
                //    {
                //        UserDocument = User.Document
                //    }
                //};

                //await bmpClient.UpsertPersonAddressAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao atualizar endereço do cliente no fundo pagador: {@event.UserId}.");

                throw;
            }
        }
    }
}
