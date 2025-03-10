using Core.Application.Messaging;
using User.Domain;

namespace User.Shared.Commands
{
    public sealed record UpsertAddressCommand(Guid UserId, Dto.Address Address) : ICommand;
}
