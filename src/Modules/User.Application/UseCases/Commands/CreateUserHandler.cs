using Core.Application.Messaging;
using Core.Shared.Results;
using User.Application.Services;
using User.Shared.Commands;
using User.Shared.Responses;

namespace User.Application.UseCases.Commands
{
    internal class CreateUserHandler(IUserApplicationService userApplicationService) : ICommandHandler<CreateUserCommand, IdentifierResponse>
    {
        public async Task<Result<IdentifierResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = Domain.Aggregates.User.Create(command.UserId, command.Document, command.Phone);

            await userApplicationService.AppendEventsAsync(user, cancellationToken);

            return Result.Success<IdentifierResponse>(new(user.Id));
        }
    }
}
