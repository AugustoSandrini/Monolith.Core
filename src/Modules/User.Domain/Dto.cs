namespace User.Domain
{
    public static class Dto
    {
        public record Address(
            string? Street,
            string? City,
            string? State,
            string? District,
            string? ZipCode,
            string? Country,
            string? Number,
            string? Complement);

        public record User(
            Guid Id,
            string Name,
            string Document,
            string Status,
            Address Address,
            string ExternalId,
            string Auth0ExternalId,
            DateTimeOffset DateOfBirth,
            DateTimeOffset? LastTokenSentAt,
            DateTimeOffset CreatedAt);

        public record CreditConsultation(
            Guid Id,
            Guid UserId,
            Guid OrderId,
            string Status,
            string DecisionIp,
            DateTimeOffset CreatedAt,
            DateTimeOffset? DecidedAt,
            DateTimeOffset? ExpireAt);

        public record PaymentProfile(
            Guid Id,
            Guid UserId,
            string ExternalId,
            string HolderName,
            string CardExpiration,
            bool AllowAsFallback,
            string CardNumberFirstSix,
            string CardNumberLastFour,
            string GatewayToken,
            DateTimeOffset CreatedAt,
            bool IsMain);

        public record EstablishmentWithOrderInProgress(
            Guid Id,
            string LegalName,
            string TradeName,
            string OrderId,
            string Status,
            DateTimeOffset CreatedAt);

        public record Email(
            Guid Id,
            string Address,
            bool IsConfirmed,
            DateTimeOffset CreatedAt);
    }
}
