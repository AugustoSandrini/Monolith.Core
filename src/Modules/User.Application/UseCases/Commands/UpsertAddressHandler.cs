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
            var userResult = await applicationService.LoadAggregateAsync<User>(cmd.UserId, cancellationToken);

            if (userResult.IsFailure)
                return Result.Failure(userResult.Error);

            var user = userResult.Value;

            user.UpsertAddress(cmd.Address);

            await applicationService.AppendEventsAsync(user, cancellationToken);

            return Result.Success();
        }
    }
}
