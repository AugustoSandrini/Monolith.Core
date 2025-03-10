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

        public record PaymentProfileCreated(
            Guid PaymentProfileId,
            Guid UserId,
            string GatewayToken,
            bool IsMainMethod,
            int ExternalId,
            DateTimeOffset CreatedAt,
            ulong Version) : Message, IDomainEvent;

        public record UserDeleted(
            Guid UserId,
            ulong Version) : Message, IDomainEvent;

        public record CreditConsultationExpired(
            Guid CreditConsultationId,
            Guid OrderId,
            DateTimeOffset ExpiredAt,
            ulong Version) : Message, IDomainEvent;

        public record CreditConsultationAccepted(
            Guid CreditConsultationId,
            Guid OrderId,
            string DecisionIp,
            DateTimeOffset DecidedAt,
            DateTimeOffset ExpireAt,
            ulong Version) : Message, IDomainEvent;

        public record CreditConsultationRefused(
            Guid CreditConsultationId,
            Guid OrderId,
            string DecisionIp,
            DateTimeOffset DecidedAt,
            ulong Version) : Message, IDomainEvent;

        public record ProfileUpdated(
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

        public record CreditConsultationCreated(
            Guid CreditConsultationId,
            Guid UserId,
            Guid OrderId,
            DateTimeOffset CreatedAt,
            ulong Version) : Message, IDomainEvent;

        public record UserVindiExternalIdLinked(
            Guid UserId,
            string VindiExternalId,
            ulong Version) : Message, IDomainEvent;

        public record LastTokenSentAtUpdated(
            Guid UserId,
            DateTimeOffset SentAt,
            ulong Version) : Message, IDomainEvent;

        public record UserDefaulterStatus(
            Guid UserId,
            string Status,
            ulong Version) : Message, IDomainEvent;

        public record UserSaleInProgressStatus(
            Guid UserId,
            string Status,
            ulong Version) : Message, IDomainEvent;

        public record UserActiveStatus(
            Guid UserId,
            string Status,
            ulong Version) : Message, IDomainEvent;
        public record UserPendingDebtStatus(
            Guid UserId,
            string Status,
            ulong Version) : Message, IDomainEvent;
    }
}
