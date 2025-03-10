using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Queries
{
    public sealed record GetCreditConsultationByOrderIdQuery(Guid UserId, Guid OrderId) : IQuery<CreditConsultationResponse>;
}
