namespace User.Shared.Responses
{
    public sealed record IdentifierResponse(Guid Id, string? Token = null);
}
