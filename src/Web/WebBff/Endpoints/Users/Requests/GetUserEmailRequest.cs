using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Users.Requests
{
    /// <summary>
    /// Represents the request for get Email by UserId.
    /// </summary>
    /// <param name="UserId">The UserId.</param>
    public sealed class GetUserEmailRequest
    {
        [FromRoute(Name = UsersRoutes.UserId)]
        public Guid UserId
        {
            get; set;
        }
    }
}
