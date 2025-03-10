using Core.Application.Messaging;
using User.Domain.Enumerations;

namespace User.Shared.Commands
{
    public sealed record UpdateCreditConsultationStatusCommand(Guid UserId, Guid OrderId, string DecisionIp, CreditConsultationStatus Status) : ICommand;
}
