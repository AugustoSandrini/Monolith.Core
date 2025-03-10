using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Queries
{
    public sealed record GetPaymentProfileQuery(Guid PaymentProfileId) : IQuery<PaymentProfileResponse>;
}
