using Microsoft.AspNetCore.Mvc;

namespace WebBff.Endpoints.Users.Requests
{
    public sealed record GetUserByDocumentRequest
    {
        [FromQuery(Name = "Document")]
        public string Document { get; set; }
    }
}
