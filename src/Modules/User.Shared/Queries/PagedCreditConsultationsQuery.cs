using Core.Application.Messaging;
using Core.Domain.Primitives;
using User.Shared.Responses;

namespace User.Shared.Queries
{
    public sealed record PagedCreditConsultationsQuery(Guid UserId, Paging Paging) : IQuery<IPagedResult<CreditConsultationResponse>>;
}
