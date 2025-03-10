using User.Application.Extensions;
using Core.Application.Messaging;
using Serilog;
using Core.Shared.Results;

namespace User.Application.UseCases.Commands
{
    using Core.Shared.Errors;
    using User.Application.Errors.Validation;
    using User.Application.Services;
    using User.Domain;
    using User.Domain.Aggregates;
    using User.Persistence.Projections;
    using User.Shared.Commands;

    public class SendTokenHandler(
        IUserApplicationService applicationService,
        //ICommunicationService communicationService,
        IUserProjection<Projection.User> projectionGateway,
        ILogger logger) : ICommandHandler<SendTokenCommand>
    {
        private const string EXPIRE_IN_MINUTES = "5";
        private const bool ENFORCE_SECURE_VALIDATION = true;
        private const string PREFIX = "CartãoSimples";

        public async Task<Result> Handle(SendTokenCommand cmd, CancellationToken cancellationToken)
        {
            var UserProjection = await projectionGateway.FindAsync(x => x.Document == cmd.Cpf.RemoveNonAlphaNumericCharacters(), cancellationToken);
            if (UserProjection is null)
                return Result.Failure(new NotFoundError(DomainError.UserNotFound));

            var UserResult = await applicationService.LoadAggregateAsync<User>(UserProjection.Id, cancellationToken);

            if (UserResult.IsFailure)
                return Result.Failure(UserResult.Error);

            var User = UserResult.Value;

            var sended = await SendSmsTokenAsync(User.Phone, cancellationToken);

            if (sended.IsFailure)
                return Result.Failure(sended.Error);

            return await PublishUserTokenSentEventAsync(User.Id, cancellationToken);
        }

        private async Task<Result> SendSmsTokenAsync(string phone, CancellationToken cancellationToken)
        {
            //var request = new SendOtpRequest(phone, PREFIX, ENFORCE_SECURE_VALIDATION, EXPIRE_IN_MINUTES);

            //try
            //{
            //    await communicationService.SendOtpAsync(request, cancellationToken);
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex, DomainError.SmsSendingFailed.Message);

            //    return Result.Failure(DomainError.SmsSendingFailed);
            //}

            return Result.Success();
        }

        private async Task<Result> PublishUserTokenSentEventAsync(Guid UserId, CancellationToken cancellationToken)
        {
            await applicationService.PublishEventAsync(
                new Event.UserTokenSent(UserId), cancellationToken);

            return Result.Success();
        }
    }
}
