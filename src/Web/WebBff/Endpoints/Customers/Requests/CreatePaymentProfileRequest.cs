using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for create a new employee.
    /// </summary>
    /// <param name="Auth0ExternalUserId">The Auth0ExternalUserId.</param>
    /// <param name="GatewayToken">The GatewayToken.</param>
    public sealed class CreatePaymentProfileRequest
    {
        [FromHeader(Name = UsersRoutes.Token)]
        public string Token
        {
            get; set;
        }

        [FromBody]
        public CreatePaymentProfileContent Content
        {
            get; set;
        }
    }

    public sealed record CreatePaymentProfileContent(string GatewayToken)
    {
    }
}
