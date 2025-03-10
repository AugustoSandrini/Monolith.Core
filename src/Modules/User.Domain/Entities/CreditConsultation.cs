using Core.Domain.Primitives;
using User.Domain.Enumerations;

namespace User.Domain.Entities;

public class CreditConsultation : Entity
{
    public Guid OrderId { get; private set; }
    public CreditConsultationStatus Status { get; private set; }
    public string DecisionIp { get; private set; }
    public DateTimeOffset? ExpireAt { get; private set; }
    public DateTimeOffset? DecidedAt { get; private set; }

    public CreditConsultation(
        Guid id, 
        Guid orderId, 
        CreditConsultationStatus status, 
        string decisionIp, 
        DateTimeOffset createdAt,
        DateTimeOffset? expireAt)
    {
        Id = id;
        OrderId = orderId;
        DecisionIp = decisionIp;
        ExpireAt = expireAt;
        CreatedAt = createdAt;
        Status = status;
    }

    public static CreditConsultation Create(Guid id, Guid orderId)
        => new(id, orderId, CreditConsultationStatus.Pending, string.Empty, DateTimeOffset.Now, null);

    public void Accept(string decisionIp, DateTimeOffset expireAt, DateTimeOffset decidedAt)
    {
        Status = CreditConsultationStatus.Accepted;
        DecisionIp = decisionIp;
        DecidedAt = decidedAt;
        ExpireAt = expireAt;
    }

    public void Refuse(string decisionIp, DateTimeOffset decidedAt)
    {
        Status = CreditConsultationStatus.Refused;
        DecisionIp = decisionIp;
        DecidedAt = decidedAt;
    }

    public void Expire()
        => Status = CreditConsultationStatus.Expired;

    public static CreditConsultation Undefined
        => new(Guid.Empty, Guid.Empty, "Undefined", "Undefined", DateTimeOffset.Now, null);
}
