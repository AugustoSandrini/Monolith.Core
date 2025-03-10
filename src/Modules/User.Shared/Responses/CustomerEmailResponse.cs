namespace User.Shared.Responses
{
    public sealed record UserEmailResponse(
        Guid Id,
        Guid UserId,
        string Address,
        bool IsConfirmed,
        DateTimeOffset CreatedAt);
}
