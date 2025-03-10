using User.Domain;
using User.Domain.Exceptions;
using Serilog;

namespace User.Application.UseCases.Events
{
    using Core.Application.EventBus;
    using User.Application.Services;
    using User.Domain.Aggregates;
    using System.Text.Json;

    public interface ICreateVindiUserWhenProfileUpdatedHandler : IEventHandler<DomainEvent.ProfileUpdated>;

    public class CreateVindiUserWhenProfileUpdatedHandler(
        IUserApplicationService applicationService,
        //IVindiClient vindiClient,
        ILogger logger) : ICreateVindiUserWhenProfileUpdatedHandler
    {
        private const string PHONE_TYPE = "mobile";

        public async Task Handle(DomainEvent.ProfileUpdated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var UserResult = await applicationService.LoadAggregateAsync<User>(@event.UserId, cancellationToken);

                if (UserResult.IsFailure)
                    throw new UserNotFoundException(@event.UserId);

                var User = UserResult.Value;

                //var request = new IncludeUserRequest
                //{
                //    Code = @event.UserId.ToString(),
                //    RegistryCode = User.Document,
                //    Email = User.Email,
                //    Name = User.Name,
                //    Phones =
                //    [
                //        new()
                //        {
                //            Number = User.Phone,
                //            PhoneType = PHONE_TYPE,
                //            Extension = null,
                //        }
                //    ]
                //};

                //logger.Error("Vindi User {Request}", JsonSerializer.Serialize(request));

                //var vindiResponse = await vindiClient.IncludeUser(request, cancellationToken);

                //logger.Error("Vindi User {Response}", JsonSerializer.Serialize(vindiResponse));

                //User.LinkExternalId(vindiResponse.User.Id.ToString());

                await applicationService.AppendEventsAsync(User, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao criar o usuário: {@event.UserId} na Vindi.");

                throw;
            }
        }
    }
}
