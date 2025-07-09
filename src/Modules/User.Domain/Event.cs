using Core.Domain.Primitives;

namespace User.Domain
{
    public static class Event
    {
        public record UserDocumentAndPhoneValidated(
            Guid UserId,
            string Document,
            string Phone,
            Guid EstablishmentId,
            decimal RequestedAmount) : Message(), IEvent;
    }
}
