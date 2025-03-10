namespace User.Shared.Responses
{
    public sealed record PaymentProfileResponse(
        Guid Id,
        string ExternalId,
        Guid UserId,
        string HolderName,
        string CardExpiration,
        bool AllowAsFallback,
        string CardNumberFirstSix,
        string CardNumberLastFour,
        string GatewayToken,
        DateTimeOffset CreatedAt,
        bool IsMain);
}
