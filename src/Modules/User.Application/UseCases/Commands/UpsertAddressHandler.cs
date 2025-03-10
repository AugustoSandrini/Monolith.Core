using Core.Application.Messaging;
using Core.Shared.Results;

namespace User.Application.UseCases.Commands
{
    using User.Application.Services;
    using User.Domain.Aggregates;
    using User.Shared.Commands;

    public class UpsertAddressHandler(IUserApplicationService applicationService) : ICommandHandler<UpsertAddressCommand>
    {
        public async Task<Result> Handle(UpsertAddressCommand cmd, CancellationToken cancellationToken)
        {
            var UserResult = await applicationService.LoadAggregateAsync<User>(cmd.UserId, cancellationToken);

            if (UserResult.IsFailure)
                return Result.Failure(UserResult.Error);

            var User = UserResult.Value;

            User.UpsertAddress(cmd.Address);

            await applicationService.AppendEventsAsync(User, cancellationToken);

            return Result.Success();
        }
    }
}
