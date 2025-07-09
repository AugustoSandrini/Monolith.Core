using Core.Application.Messaging;

namespace User.Shared.Commands
{
    public sealed record UpdateProfileCommand(string Document, string Name, string Email, DateTimeOffset DateOfBirth, string Password) : ICommand;
}
