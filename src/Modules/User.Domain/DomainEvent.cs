using Core.Domain.Primitives;

namespace User.Domain
{
    public static class DomainEvent
    {
        public record UserCreated(
            Guid UserId,
            string Document,
            string Phone,
            string Status,
            DateTimeOffset CreatedAt,
            ulong Version) : Message, IDomainEvent;

        public record UserDeleted(
            Guid UserId,
            ulong Version) : Message, IDomainEvent;

        public record UserUpdated(
            Guid UserId,
            string Name,
            string Email,
            string Status,
            DateTimeOffset DateOfBirth,
            ulong Version) : Message, IDomainEvent;

        public record AddressUpserted(
            Guid UserId,
            Dto.Address Address,
            ulong Version) : Message, IDomainEvent;

        public record UserDefaulterStatus(
            Guid UserId,
            string Status,
            ulong Version) : Message, IDomainEvent;

        public record UserActiveStatus(
            Guid UserId,
            string Status,
            ulong Version) : Message, IDomainEvent;
    }
}
