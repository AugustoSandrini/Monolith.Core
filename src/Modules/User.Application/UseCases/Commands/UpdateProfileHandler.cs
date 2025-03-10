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
    using System.Net;

    public class UpdateProfileHandler(
        IUserApplicationService applicationService,
        //ICommunicationService communicationService,
        IUserProjection<Projection.User> UserProjectionGateway,
        IUserProjection<Projection.Email> projectionGateway,
        //IAuth0ApiClient auth0ApiService,
        ILogger logger) : ICommandHandler<UpdateProfileCommand>
    {
        //public const string CONNECTION_TYPE = "User";

        public async Task<Result> Handle(UpdateProfileCommand cmd, CancellationToken cancellationToken)
        {
            var UserProjection = await UserProjectionGateway.FindAsync(User => User.Document == cmd.Document.RemoveNonAlphaNumericCharacters(), cancellationToken);
            if (UserProjection is null)
                return Result.Failure(new NotFoundError(DomainError.UserNotFound));
            
            if(UserProjection.Status != UserStatus.PendingProfile)
                return Result.Failure(new ConflictError(DomainError.UserStatusNowAllowed));

            var validationResult = await ValidateUserEmailAsync(cmd.Email, UserProjection.Id, cancellationToken);

            if (validationResult.IsFailure)
                return Result.Failure(validationResult.Error);

            var UserResult = await applicationService.LoadAggregateAsync<User>(UserProjection.Id, cancellationToken); ;

            if(UserResult.IsFailure)
                return Result.Failure(UserResult.Error);

            var User = UserResult.Value;

            var verified = await VerifySmsTokenAsync(User.Phone, cmd.Token, cancellationToken);

            if (verified.IsFailure)
                return Result.Failure(verified.Error);

            var userIncluded = await IncludeAuth0UserAsync(cmd, User.Id.ToString(), cancellationToken);

            if (userIncluded.IsFailure)
                return Result.Failure(userIncluded.Error);

            User.UpdateProfile(cmd.Name, cmd.Email, cmd.DateOfBirth);

            await applicationService.AppendEventsAsync(User, cancellationToken);

            return Result.Success();
        }

        private async Task<Result> VerifySmsTokenAsync(string phone, string token, CancellationToken cancellationToken)
        {
            //var request = new VerifyOtpRequest(token, phone);

            //try
            //{
            //    await communicationService.VerifyOtpAsync(request, cancellationToken);
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex, DomainError.SmsSendingFailed.Message);

            //    return Result.Failure(DomainError.SmsSendingFailed);
            //}

            return Result.Success();
        }

        private async Task<Result> IncludeAuth0UserAsync(UpdateProfileCommand cmd, string id, CancellationToken cancellationToken)
        {
            //var request = new IncludeUserRequest()
            //{
            //    Id = id,
            //    Name = cmd.Name,
            //    Email = cmd.Email,
            //    Password = cmd.Password,
            //    Connection = CONNECTION_TYPE
            //};

            //try
            //{
            //    await auth0ApiService.IncludeUserAsync(request, cancellationToken);
            //}
            //catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            //{
            //    var auth0users = await auth0ApiService.GetUserByEmailAsync(cmd.Email, cancellationToken);

            //    var user = auth0users.First(user => user.Identities.Any(identity => identity.Connection.Equals(CONNECTION_TYPE, StringComparison.OrdinalIgnoreCase)));

            //    await auth0ApiService.DeleteUserAsync(user.UserId, cancellationToken);

            //    await IncludeAuth0UserAsync(cmd, id, cancellationToken);
            //}
            //catch (ApiException ex)
            //{
            //    var error = new Error(DomainError.Auth0IncludingUserFailed.Code, DomainError.Auth0IncludingUserFailed.Message);

            //    logger.Error(ex, error.Message);

            //    return Result.Failure(error);
            //}

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
