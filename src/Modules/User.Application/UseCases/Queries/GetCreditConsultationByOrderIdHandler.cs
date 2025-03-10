using Core.Application.Messaging;
using Core.Shared.Results;
using Core.Shared.Errors;
using User.Domain;
using User.Shared.Queries;
using User.Shared.Responses;
using User.Application.Errors.Validation;
using User.Persistence.Projections;

namespace User.Application.UseCases.Queries
{
    public class GetCreditConsultationByOrderIdHandler(
        IUserProjection<Projection.CreditConsultation> creditConsultationProjection) : IQueryHandler<GetCreditConsultationByOrderIdQuery, CreditConsultationResponse>
    {
        public async Task<Result<CreditConsultationResponse>> Handle(GetCreditConsultationByOrderIdQuery query, CancellationToken cancellationToken)
        {
            var creditConsultation = await creditConsultationProjection.FindAsync(creditConsultation => creditConsultation.UserId == query.UserId && creditConsultation.OrderId == query.OrderId, cancellationToken);
            
            if(creditConsultation is null)
                return Result.Failure<CreditConsultationResponse>(new NotFoundError(DomainError.CreditConsultationNotFound));

            return Result.Success<CreditConsultationResponse>(
                new(creditConsultation.Id, 
                    creditConsultation.OrderId, 
                    creditConsultation.UserId,
                    creditConsultation.Status,
                    creditConsultation.DecisionIp,
                    creditConsultation.CreatedAt,
                    creditConsultation.DecidedAt,
                    creditConsultation.ExpireAt));
        }
    }
}
