using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Domain.Primitives;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using User.Shared.Queries;
using User.Shared.Responses;
using WebBff.Endpoints.Routes;
using WebBff.Endpoints.Users.Requests;

namespace WebBff.Endpoints.Users
{
    public class PagedUserEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<PagedUserRequest>
        .WithActionResult<IPagedResult<UserResponse>>
    {
        [ApiVersion("1.0")]
        [HttpGet(UsersRoutes.PagedUser)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerOperation(
            Summary = "Paged a User.",
            Description = "Paged a User based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<IPagedResult<UserResponse>>> HandleAsync(
            PagedUserRequest request,
            CancellationToken cancellationToken = default) =>
            await Result.Create(request)
            .Map(r => new PagedUserQuery(new(r.Limit, r.Offset)))
            .Bind(query => sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
