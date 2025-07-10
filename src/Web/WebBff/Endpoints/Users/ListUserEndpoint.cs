using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using User.Shared.Queries;
using User.Shared.Responses;
using WebBff.Endpoints.Routes;
using WebBff.Endpoints.Users.Requests;

namespace WebBff.Endpoints.Users
{
    public class ListUserEndpoint(ISender sender) : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<UserResponse>>
    {
        [ApiVersion("1.0")]
        [HttpGet(UsersRoutes.ListUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(
            Summary = "List Users.",
            Description = "List Users based on the provided request data.",
            Tags = [Tags.Users])]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<List<UserResponse>>> HandleAsync(CancellationToken cancellationToken = default) =>
            await Result.Create(string.Empty)
            .Map(r => new ListUserQuery())
            .Bind(query => sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
