using User.Domain;

namespace User.Shared.Responses
{
    public sealed record UserResponse(
        Guid Id,
        string? Name,
        string Document,
        string? Phone,
        string? Email,
        string Status,
        Dto.Address? Address,
        DateTimeOffset? DateOfBirth,
        DateTimeOffset CreatedAt)
    {
        public static implicit operator Dto.User(UserResponse User)
            => new(User.Id, User.Name, User.Document, User.Status, User.Address, User.DateOfBirth!.Value, User.CreatedAt);
    };
}
