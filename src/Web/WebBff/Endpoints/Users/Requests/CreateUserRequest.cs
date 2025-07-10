using Microsoft.AspNetCore.Mvc;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Users.Requests
{
    public sealed class CreateUserRequest
    {
        [FromBody]
        public Content Content { get; set; }
    }

    public record Content(string Document, string Phone);
}
