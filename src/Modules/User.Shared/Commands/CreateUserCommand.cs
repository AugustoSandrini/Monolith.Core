using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Commands
{
    public record CreateUserCommand(Guid UserId, string Document, string Phone, string Email) : ICommand<IdentifierResponse>;
}
