using Microsoft.AspNetCore.Mvc;

namespace WebBff.Endpoints.Users.Requests
{
    public sealed record ListUserByNameRequest
    {
        [FromQuery]
        public string Name { get; set; }
    }
}
