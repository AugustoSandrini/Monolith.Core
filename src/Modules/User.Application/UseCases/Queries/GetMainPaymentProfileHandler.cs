using Core.Application.Messaging;
using Core.Shared.Errors;
using Core.Shared.Results;
using User.Application.Errors.Validation;
using User.Domain;
using User.Persistence.Projections;
using User.Shared.Queries;
using User.Shared.Responses;

namespace User.Application.UseCases.Queries
{

    public class GetMainPaymentProfileHandler(IUserProjection<Projection.PaymentProfile> paymentProfileProjection) : IQueryHandler<GetMainPaymentProfileQuery, PaymentProfileResponse>
    {
        public async Task<Result<PaymentProfileResponse>> Handle(GetMainPaymentProfileQuery query, CancellationToken cancellationToken)
        {
            var mainPaymentMethod = await paymentProfileProjection.FindAsync(paymentProfile => paymentProfile.UserId == query.UserId && paymentProfile.IsMain, cancellationToken);

            if (mainPaymentMethod is null)
                return Result.Failure<PaymentProfileResponse>(new NotFoundError(DomainError.PaymentMethodNotFound));

            return Result.Success<PaymentProfileResponse>(
                new(mainPaymentMethod.Id,
                    mainPaymentMethod.VindiExternalId,
                    mainPaymentMethod.UserId,
                    mainPaymentMethod.HolderName,
                    mainPaymentMethod.CardExpiration,
                    mainPaymentMethod.AllowAsFallback,
                    mainPaymentMethod.CardNumberFirstSix,
                    mainPaymentMethod.CardNumberLastFour,
                    mainPaymentMethod.GatewayToken,
                    mainPaymentMethod.CreatedAt,
                    mainPaymentMethod.IsMain));
        }
    }
}
