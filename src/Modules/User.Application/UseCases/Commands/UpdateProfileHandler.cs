using Core.Application.Messaging;
using Core.Shared.Results;
using Serilog;
using Core.Shared.Errors;
using User.Application.Errors.Validation;

namespace User.Application.UseCases.Commands
{
    using User.Application.Extensions;
    using User.Application.Services;
    using User.Domain;
    using User.Domain.Aggregates;
    using User.Domain.Enumerations;
    using User.Persistence.Projections;
    using User.Shared.Commands;

    public class UpdateProfileHandler(
        IUserApplicationService applicationService,
        IUserProjection<Projection.User> userProjectionGateway,
        IUserProjection<Projection.Email> projectionGateway) : ICommandHandler<UpdateProfileCommand>
    {
        public async Task<Result> Handle(UpdateProfileCommand cmd, CancellationToken cancellationToken)
        {
            var userProjection = await userProjectionGateway.FindAsync(user => user.Document == cmd.Document.RemoveNonAlphaNumericCharacters(), cancellationToken);

            if(userProjection.Status != UserStatus.Default) return Result.Failure(new ConflictError(DomainError.UserStatusNowAllowed));

            if (userProjection is null) return Result.Failure(new NotFoundError(DomainError.UserNotFound));

            var validationResult = await ValidateUserEmailAsync(cmd.Email, userProjection.Id, cancellationToken);

            if (validationResult.IsFailure) return Result.Failure(validationResult.Error);

            var UserResult = await applicationService.LoadAggregateAsync<User>(userProjection.Id, cancellationToken);

            if(UserResult.IsFailure) return Result.Failure(UserResult.Error);

            var User = UserResult.Value;

            User.UpdateProfile(cmd.Name, cmd.Email, cmd.DateOfBirth);

            await applicationService.AppendEventsAsync(User, cancellationToken);

            return Result.Success();
        }

        private async Task<Result> ValidateUserEmailAsync(string email, Guid UserId, CancellationToken cancellationToken)
        {
            var emailProjection = await projectionGateway.FindAsync(x => x.Address == email && x.UserId != UserId, cancellationToken);

            return emailProjection is not null ? Result.Failure<User>(new ConflictError(DomainError.EmailAlreadyAdded)) : Result.Success();
        }
    }
}
