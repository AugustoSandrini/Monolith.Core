using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using User.Shared.Queries;
using User.Shared.Responses;
using WebBff.Endpoints.Routes;
using WebBff.Endpoints.Users.Requests;

namespace WebBff.Endpoints.Users
{
    public class GetUserByDocumentEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<GetUserByDocumentRequest>
        .WithActionResult<UserResponse>
    {
        [ApiVersion("1.0")]
        [HttpGet(UsersRoutes.GetUserByDocument)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Get a User.",
            Description = "Get a User based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult<UserResponse>> HandleAsync(
            GetUserByDocumentRequest request,
            CancellationToken cancellationToken = default) =>
            await Result.Create(request)
            .Map(r => new GetUserByDocumentQuery(r.Document))
            .Bind(query => sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
