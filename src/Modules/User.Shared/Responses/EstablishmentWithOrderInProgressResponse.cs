namespace User.Shared.Responses
{
    public sealed record EstablishmentWithOrderInProgressResponse(
        Guid Id,
        string LegalName,
        string TradeName,
        Guid OrderId,
        string Status,
        DateTimeOffset CreatedAt);
}
