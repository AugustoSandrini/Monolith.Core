namespace User.Shared.Responses
{
    public sealed record UserPhoneResponse(
        Guid Id,
        Guid UserId,
        string Address,
        bool IsConfirmed,
        DateTimeOffset CreatedAt);
}
