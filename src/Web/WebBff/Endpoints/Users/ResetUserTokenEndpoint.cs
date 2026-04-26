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
using WebBff.Services;

namespace WebBff.Endpoints.Users
{
    public sealed class ResetUserTokenEndpoint(ISender sender, ITokenService tokenService) : EndpointBaseAsync
        .WithRequest<ResetUserTokenRequest>
        .WithActionResult<IdentifierResponse>
    {
        [ApiVersion("1.0")]
        [HttpPost(UsersRoutes.ResetToken)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Reset User Token",
            Description = "Issues a new JWT token for the given user.",
            Tags = [Tags.Users])]
        public override async Task<ActionResult<IdentifierResponse>> HandleAsync(
            ResetUserTokenRequest request,
            CancellationToken cancellationToken = default) =>
            await Result.Create(request)
                .Map(r => new GetUserByIdQuery(r.UserId))
                .Bind(query => sender.Send(query, cancellationToken))
                .Map(user => new IdentifierResponse(user.Id, tokenService.GenerateToken(user.Id.ToString())))
                .Match(Ok, this.HandleFailure);
    }
}
