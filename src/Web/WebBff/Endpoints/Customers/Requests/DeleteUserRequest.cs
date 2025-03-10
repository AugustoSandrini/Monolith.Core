using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Customers.Requests
{
    /// <summary>
    /// Represents the request for delete a User.
    /// </summary>
    /// <param name="UserId">The UserId.</param>
    public sealed class DeleteUserRequest
    {
        [FromRoute(Name = UsersRoutes.UserId)]
        public Guid UserId
        {
            get; set;
        }
    }
}
