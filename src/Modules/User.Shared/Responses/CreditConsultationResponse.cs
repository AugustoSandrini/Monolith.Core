namespace User.Shared.Responses
{
    public sealed record CreditConsultationResponse(
        Guid Id,
        Guid OrderId,
        Guid UserId,
        string Status,
        string DecisionIp,
        DateTimeOffset CreatedAt,
        DateTimeOffset DecidedAt,
        DateTimeOffset ExpireAt);
}
