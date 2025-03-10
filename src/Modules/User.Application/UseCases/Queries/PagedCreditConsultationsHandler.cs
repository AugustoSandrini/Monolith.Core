using Core.Application.Messaging;
using Core.Application.Pagination;
using Core.Domain.Primitives;
using Core.Shared.Results;
using User.Domain;
using User.Persistence.Projections;
using User.Shared.Queries;
using User.Shared.Responses;

namespace User.Application.UseCases.Queries
{
    public class PagedCreditConsultationsHandler(
        IUserProjection<Projection.CreditConsultation> creditConsultationProjection) : IQueryHandler<PagedCreditConsultationsQuery, IPagedResult<CreditConsultationResponse>>
    {
        public async Task<Result<IPagedResult<CreditConsultationResponse>>> Handle(PagedCreditConsultationsQuery query, CancellationToken cancellationToken)
        {
            var creditConsultations = await creditConsultationProjection.ListAsync(creditConsultation => creditConsultation.UserId == query.UserId, cancellationToken);

            return creditConsultations is not null
                ? Result.Success(PagedResult<CreditConsultationResponse>.Create(query.Paging, creditConsultations.Select(cc =>
                    new CreditConsultationResponse(cc.Id, cc.OrderId, cc.UserId, cc.Status, cc.DecisionIp, cc.CreatedAt, cc.DecidedAt, cc.ExpireAt)).AsQueryable()))
                : Result.Success<IPagedResult<CreditConsultationResponse>>(default);
        }
    }
}
