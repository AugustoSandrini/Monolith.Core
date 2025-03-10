using Core.Application.Messaging;
using Core.Shared.Results;

namespace User.Application.UseCases.Commands
{
    using User.Application.Services;
    using User.Domain.Aggregates;
    using User.Shared.Commands;

    internal class DeleteUserHandler(IUserApplicationService applicationService) : ICommandHandler<DeleteUserCommand>
    {
        public async Task<Result> Handle(DeleteUserCommand cmd, CancellationToken cancellationToken)
        {
            var UserResult = await applicationService.LoadAggregateAsync<User>(cmd.UserId, cancellationToken);

            if (UserResult.IsFailure)
                return Result.Failure(UserResult.Error);

            var User = UserResult.Value;

            User.Delete();

            await applicationService.AppendEventsAsync(User, cancellationToken);

            return Result.Success();
        }
    }
}
