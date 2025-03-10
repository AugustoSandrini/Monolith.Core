using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for update CreditConsultation status.
    /// </summary>
    /// <param name="Token">The idToken.</param>
    /// <param name="OrderId">The OrderId.</param>
    /// <param name="DecisionIp">The DecisionIp.</param>
    /// <param name="Status">The CreditConsultationStatus.</param>
    public sealed class UpdateCreditConsultationStatusRequest
    {
        [FromHeader(Name = UsersRoutes.Token)]
        public string Token
        {
            get; set;
        }

        [FromBody]
        public UpdateCreditConsultationStatusContent Content
        {
            get; set;
        }
    }

    public record UpdateCreditConsultationStatusContent(Guid OrderId, string Accepted)
    {
    }
}
