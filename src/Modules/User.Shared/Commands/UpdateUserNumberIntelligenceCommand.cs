using Core.Application.Messaging;

namespace User.Shared.Commands
{
    public sealed record UpdateUserNumberIntelligenceCommand(string Token, string Matched) : ICommand;
}
