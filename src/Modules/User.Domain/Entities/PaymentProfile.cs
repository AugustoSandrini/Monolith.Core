using Core.Domain.Primitives;

namespace User.Domain.Entities;

public class PaymentProfile : Entity
{
    public string VindiExternalId { get; private set; }
    public string HolderName { get; private set; }
    public string CardExpiration { get; private set; }
    public bool AllowAsFallback { get; private set; }
    public string CardNumberFirstSix { get; private set; }
    public string CardNumberLastFour { get; private set; }
    public string GatewayToken { get; set; }
    public bool IsMain { get; set; }

    public PaymentProfile(
        Guid id, 
        string externalId, 
        string holderName, 
        string cardExpiration, 
        bool allowAsFallback, 
        string cardNumberFirstSix, 
        string cardNumberLastFour, 
        string gatewayToken, 
        bool isMain)
    {
        Id = id;
        VindiExternalId = externalId;
        HolderName = holderName;
        CardExpiration = cardExpiration;
        AllowAsFallback = allowAsFallback;
        CardNumberFirstSix = cardNumberFirstSix;
        CardNumberLastFour = cardNumberLastFour;
        GatewayToken = gatewayToken;
        IsMain = isMain;
    }

    public static PaymentProfile Create(Guid id, int externalId, string gateWayToken)
        => new(id, externalId.ToString(), string.Empty, string.Empty, false, string.Empty, string.Empty, gateWayToken, true);

    public void SetAsNotMain()
        => IsMain = false;

    public static PaymentProfile Undefined
        => new(Guid.Empty, "Undefined", "Undefined", "Undefined", false, "Undefined", "Undefined", "Undefined", false);
}
