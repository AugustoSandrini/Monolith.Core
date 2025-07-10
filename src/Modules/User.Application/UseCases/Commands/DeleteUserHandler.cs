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
            var userResult = await applicationService.LoadAggregateAsync<User>(cmd.UserId, cancellationToken);

            if (userResult.IsFailure)
                return Result.Failure(userResult.Error);

            var user = userResult.Value;

            user.Delete();

            await applicationService.AppendEventsAsync(user, cancellationToken);

            return Result.Success();
        }
    }
}
