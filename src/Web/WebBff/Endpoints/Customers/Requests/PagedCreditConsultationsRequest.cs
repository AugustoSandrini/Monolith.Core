using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for Paged CreditConsultations.
    /// </summary>
    /// <param name="Token">The idToken.</param>
    public sealed class PagedCreditConsultationsRequest
    {
        [FromHeader(Name = UsersRoutes.Token)]
        public string Token
        {
            get; set;
        }

        [FromQuery]
        public int Offset { get; set; } = 1;

        [FromQuery]
        public int Limit { get; set; } = 10;
    }
}
