using Ardalis.SmartEnum;
using Newtonsoft.Json;

namespace User.Domain.Enumerations
{
    [method: JsonConstructor]
    public class CreditConsultationStatus(string name, int value) : SmartEnum<CreditConsultationStatus>(name, value)
    {
        public static readonly CreditConsultationStatus Pending = new PendingStatus();
        public static readonly CreditConsultationStatus Accepted = new AcceptedStatus();
        public static readonly CreditConsultationStatus Refused = new RefusedStatus();
        public static readonly CreditConsultationStatus Expired = new ExpiredStatus();

        public static implicit operator CreditConsultationStatus(string name)
            => FromName(name);

        public static implicit operator CreditConsultationStatus(int value)
            => FromValue(value);

        public static implicit operator string(CreditConsultationStatus status)
            => status.Name;

        public static implicit operator int(CreditConsultationStatus status)
            => status.Value;

        public class PendingStatus() : CreditConsultationStatus(nameof(Pending), 0) { }
        public class AcceptedStatus() : CreditConsultationStatus(nameof(Accepted), 1) { }
        public class RefusedStatus() : CreditConsultationStatus(nameof(Refused), 2) { }
        public class ExpiredStatus() : CreditConsultationStatus(nameof(Expired), 3) { }
    }
}
