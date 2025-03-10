using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for List Order With Establishment In Progress.
    /// </summary>
    /// <param name="Token">The idToken.</param>
    public sealed class GetPurchasedOrderByUserIdRequest
    {
        [FromHeader(Name = UsersRoutes.Token)]
        public string Token
        {
            get; set;
        }
    }
}
