using Core.Application.Messaging;

namespace User.Shared.Commands
{
    public sealed record ValidateUserDocumentPhoneCommand(string Document, string Phone, Guid EstablishmentId, decimal RequestedAmount) : ICommand;
}
