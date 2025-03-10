using Core.Domain.Primitives;

namespace User.Domain
{
    public static class Projection
    {
        public record User(
            Guid Id,
            string Name,
            string Document,
            string Status,
            Dto.Address? Address,
            string VindiExternalId,
            DateTimeOffset DateOfBirth,
            DateTimeOffset? LastTokenSentAt,
            DateTimeOffset CreatedAt) : IProjection
        { }

        public record Email(
            Guid Id,
            Guid UserId,
            string Address,
            bool IsConfirmed,
            DateTimeOffset CreatedAt) : IProjection
        { }

        public record Phone(
            Guid Id,
            Guid UserId,
            string Number,
            bool IsConfirmed,
            DateTimeOffset CreatedAt) : IProjection
        { }

        public record PaymentProfile(
            Guid Id,
            string VindiExternalId,
            Guid UserId,
            string HolderName,
            string CardExpiration,
            bool AllowAsFallback,
            string CardNumberFirstSix,
            string CardNumberLastFour,
            string GatewayToken,
            DateTimeOffset CreatedAt,
            bool IsMain) : IProjection
        {
            public static implicit operator Dto.PaymentProfile(PaymentProfile paymentProfile)
                => new(
                    paymentProfile.Id,
                    paymentProfile.UserId,
                    paymentProfile.VindiExternalId,
                    paymentProfile.HolderName,
                    paymentProfile.CardExpiration,
                    paymentProfile.AllowAsFallback,
                    paymentProfile.CardNumberFirstSix,
                    paymentProfile.CardNumberLastFour,
                    paymentProfile.GatewayToken,
                    paymentProfile.CreatedAt,
                    paymentProfile.IsMain);
        }

        public record CreditConsultation(
            Guid Id,
            Guid OrderId,
            Guid UserId,
            string Status,
            string DecisionIp,
            DateTimeOffset DecidedAt,
            DateTimeOffset ExpireAt,
            DateTimeOffset CreatedAt) : IProjection
        {
            public static implicit operator Dto.CreditConsultation(CreditConsultation creditConsultation)
                => new(creditConsultation.Id, creditConsultation.UserId, creditConsultation.OrderId, creditConsultation.Status, creditConsultation.DecisionIp, creditConsultation.CreatedAt, creditConsultation.DecidedAt, creditConsultation.ExpireAt);
        }
    }
}
