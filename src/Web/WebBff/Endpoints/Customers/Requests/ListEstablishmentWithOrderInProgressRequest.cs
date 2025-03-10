using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for List Establishment With Order In Progress.
    /// </summary>
    /// <param name="Token">The idToken.</param>
    public sealed class ListEstablishmentWithOrderInProgressRequest
    {
        [FromHeader(Name = UsersRoutes.Token)]
        public string Token
        {
            get; set;
        }
    }
}
