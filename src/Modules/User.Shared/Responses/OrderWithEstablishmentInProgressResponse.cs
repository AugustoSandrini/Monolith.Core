namespace User.Shared.Responses
{
    public sealed record OrderWithEstablishmentInProgressResponse(
        string TradeName,
        long OrderNumber,
        Guid OrderId,
        decimal OrderAmount,
        Guid EstablishmentId,
        string OrderStatus,
        DateTimeOffset CreatedAt);
}
