using Core.Application.Messaging;

namespace User.Shared.Commands
{
    public sealed record SendTokenCommand(string Cpf) : ICommand;
}
