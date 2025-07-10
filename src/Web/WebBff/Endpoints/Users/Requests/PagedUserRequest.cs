using Microsoft.AspNetCore.Mvc;

namespace WebBff.Endpoints.Users.Requests
{
    public sealed record PagedUserRequest
    {
        [FromQuery]
        public int Offset { get; set; } = 1;

        [FromQuery]
        public int Limit { get; set; } = 10;
    }
}
