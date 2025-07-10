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
        IUserProjection<Projection.User> UserProjectionGateway,
        IUserProjection<Projection.Email> projectionGateway) : ICommandHandler<UpdateProfileCommand>
    {
        public async Task<Result> Handle(UpdateProfileCommand cmd, CancellationToken cancellationToken)
        {
            var UserProjection = await UserProjectionGateway.FindAsync(User => User.Document == cmd.Document.RemoveNonAlphaNumericCharacters(), cancellationToken);

            if (UserProjection is null) return Result.Failure(new NotFoundError(DomainError.UserNotFound));
            
            if(UserProjection.Status != UserStatus.Default) return Result.Failure(new ConflictError(DomainError.UserStatusNowAllowed));

            var validationResult = await ValidateUserEmailAsync(cmd.Email, UserProjection.Id, cancellationToken);

            if (validationResult.IsFailure) return Result.Failure(validationResult.Error);

            var UserResult = await applicationService.LoadAggregateAsync<User>(UserProjection.Id, cancellationToken);

            if(UserResult.IsFailure) return Result.Failure(UserResult.Error);

            var User = UserResult.Value;

            User.UpdateProfile(cmd.Name, cmd.Email, cmd.DateOfBirth);

            await applicationService.AppendEventsAsync(User, cancellationToken);

            return Result.Success();
        }

        private async Task<Result> ValidateUserEmailAsync(string email, Guid UserId, CancellationToken cancellationToken)
        {
            var emailProjection = await projectionGateway.FindAsync(x => x.Address == email && x.UserId != UserId, cancellationToken);

            if (emailProjection is not null)
                return Result.Failure<User>(new ConflictError(DomainError.EmailAlreadyAdded));

            return Result.Success();
        }
    }
}
