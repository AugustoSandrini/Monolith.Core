using Core.Application.Messaging;

namespace User.Shared.Commands
{
    public sealed record DeleteUserCommand(Guid UserId) : ICommand;
}
