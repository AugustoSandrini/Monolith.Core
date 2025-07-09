using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Commands
{
    public record CreateUserCommand(string Document, string Phone) : ICommand<IdentifierResponse>;
}
