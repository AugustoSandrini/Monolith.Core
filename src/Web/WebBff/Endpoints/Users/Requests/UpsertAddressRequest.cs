using Microsoft.AspNetCore.Mvc;
using User.Domain;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Users.Requests
{
    /// <summary>
    /// Represents the request for upsert Address.
    /// </summary>
    /// <param name="Token">The idToken.</param>
    /// <param name="Address">The Address.</param>
    public sealed class UpsertAddressRequest
    {
        [FromQuery(Name = UsersRoutes.UserId)]
        public Guid UserId { get; set; }

        [FromBody]
        public UpsertAddressContent Content
        {
            get; set;
        }
    }

    public record UpsertAddressContent(Dto.Address Address)
    {
    }
}
