using User.Domain.Exceptions;
using User.Domain;
using MediatR;
using Serilog;

namespace User.Application.UseCases.Events
{
    using Core.Application.EventBus;
    using User.Application.Services;
    using User.Domain.Aggregates;
    using User.Domain.Enumerations;
    using User.Shared.Commands;

    public interface ISendSmsTokenWhenCreditConsultationCreatedHandler : IEventHandler<DomainEvent.CreditConsultationCreated>;

    public class SendSmsTokenWhenCreditConsultationCreatedHandler(
       IUserApplicationService applicationService,
       ISender sender,
       ILogger logger) : ISendSmsTokenWhenCreditConsultationCreatedHandler
    {

        public async Task Handle(DomainEvent.CreditConsultationCreated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var UserResult = await applicationService.LoadAggregateAsync<User>(@event.UserId, cancellationToken);

                if (UserResult.IsFailure)
                    throw new UserNotFoundException(@event.UserId);

                var User = UserResult.Value;

                if(User.Status == UserStatus.PendingProfile)
                    await sender.Send(new SendTokenCommand(User.Document), cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao enviar token de sms para o cliente: {@event.UserId}.");

                throw;
            }
        }
    }
}
