using Core.Domain.Primitives;

namespace User.Domain
{
    public static class Projection
    {
        public record User(
            Guid Id,
            string Name,
            string Document,
            string Status,
            Dto.Address? Address,
            DateTimeOffset DateOfBirth,
            DateTimeOffset CreatedAt) : IProjection
        { }

        public record Email(
            Guid Id,
            Guid UserId,
            string Address,
            bool IsConfirmed,
            DateTimeOffset CreatedAt) : IProjection
        { }

        public record Phone(
            Guid Id,
            Guid UserId,
            string Number,
            bool IsConfirmed,
            DateTimeOffset CreatedAt) : IProjection
        { }
    }
}
