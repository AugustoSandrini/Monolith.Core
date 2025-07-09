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
            DateTimeOffset DateOfBirth,
            DateTimeOffset CreatedAt);

        public record Email(
            Guid Id,
            string Address,
            bool IsConfirmed,
            DateTimeOffset CreatedAt);
    }
}
