using User.Domain;
using Serilog;

namespace User.Application.UseCases.Events
{
    using Core.Application.EventBus;
    using User.Application.Services;
    using User.Domain.Aggregates;
    using User.Domain.Exceptions;

    public interface IUpdateLastTokenSentAtWhenTokenSentHandler : IEventHandler<DomainEvent.UserVindiExternalIdLinked>;

    public class UpdateLastTokenSentAtWhenTokenSentHandler(
        IUserApplicationService applicationService,
        ILogger logger) : IUpdateLastTokenSentAtWhenTokenSentHandler
    {

        public async Task Handle(DomainEvent.UserVindiExternalIdLinked @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var UserResult = await applicationService.LoadAggregateAsync<User>(@event.UserId, cancellationToken);

                if (UserResult.IsFailure)
                    throw new UserNotFoundException(@event.UserId);

                var User = UserResult.Value;

                User.UpdateLastTokenSentAt();

                await applicationService.AppendEventsAsync(User, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao atualizar data de último token de sms enviada para o cliente: {@event.UserId}.");

                throw;
            }
        }
    }
}
