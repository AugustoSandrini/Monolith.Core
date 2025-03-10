using User.Domain;
using Serilog;

namespace User.Application.UseCases.Events
{
    using Core.Application.EventBus;
    using User.Application.Services;
    using User.Domain.Aggregates;
    using User.Domain.Exceptions;
    using Newtonsoft.Json;

    public interface ISendToBmpWhenProfileUpdatedHandler : IEventHandler<DomainEvent.ProfileUpdated>;

    public class SendToBmpWhenProfileUpdatedHandler(
        //IBmpClient bmpClient,
        IUserApplicationService applicationService,
        ILogger logger) : ISendToBmpWhenProfileUpdatedHandler
    {

        public async Task Handle(DomainEvent.ProfileUpdated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var UserResult = await applicationService.LoadAggregateAsync<User>(@event.UserId, cancellationToken);

                if (UserResult.IsFailure)
                    throw new UserNotFoundException(@event.UserId);

                var User = UserResult.Value;

                var firstName = User.Name.Split(' ').FirstOrDefault();
                var lastName = User.Name.Split(' ').Skip(1).LastOrDefault();

                Log.Error($"LocalDateTime {JsonConvert.SerializeObject(@event.DateOfBirth.LocalDateTime)}");

                //var request = new UpsertPersonRequest()
                //{
                //    Person = new()
                //    {
                //        Name = $"{firstName} {lastName}",
                //        NaturalPerson = new()
                //        {
                //            Nickname = firstName!,
                //            DateOfBirth = @event.DateOfBirth.LocalDateTime
                //        },
                //        PersonContact = new()
                //        {
                //            Email = @event.Email,
                //            Phone = User.Phone.Remove(0, 3)
                //        },
                //        SocialSecurityNumber = User.Document
                //    }
                //};

                //await bmpClient.UpsertPersonAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao atualizar perfil do cliente no fundo pagador: {@event.UserId}.");

                throw;
            }
        }
    }
}
