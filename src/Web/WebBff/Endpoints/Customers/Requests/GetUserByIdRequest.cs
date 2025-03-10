using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for get User by UserId.
    /// </summary>
    /// <param name="UserId">The UserId.</param>
    public sealed class GetUserByIdRequest
    {
        [FromHeader(Name = UsersRoutes.Token)]
        public string Token
        {
            get; set;
        }
    }
}
