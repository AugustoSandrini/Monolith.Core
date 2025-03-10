using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Commands
{
    public sealed record CreatePaymentProfileCommand(Guid UserId, string GatewayToken) : ICommand<IdentifierResponse>;
}
