using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Users.Requests
{
    public sealed class ResetUserTokenRequest
    {
        [FromRoute(Name = UsersRoutes.UserId)]
        public Guid UserId { get; set; }
    }
}
