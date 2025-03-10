using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for get creditConsultation by OrderId.
    /// </summary>
    /// <param name="Token">The idToken.</param>
    /// <param name="OrderId">The OrderId.</param>
    public sealed class GetCreditConsultationByOrderIdRequest
    {
        [FromHeader(Name = UsersRoutes.Token)]
        public string Token
        {
            get; set;
        }

        [FromRoute]
        public Guid OrderId
        {
            get; set;
        }
    }
}
