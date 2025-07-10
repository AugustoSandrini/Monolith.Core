using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using User.Shared.Commands;
using User.Shared.Responses;
using WebBff.Endpoints.Routes;
using WebBff.Endpoints.Users.Requests;
using WebBff.Services;

namespace WebBff.Endpoints.Users
{
    public sealed class CreateUserEndpoint(ISender sender, ITokenService tokenService) : EndpointBaseAsync
        .WithRequest<CreateUserRequest>
        .WithActionResult<IdentifierResponse>
    {
        [ApiVersion("1.0")]
        [HttpPost(UsersRoutes.CreateUser)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Create a User.",
            Description = "Create a User based on the provided request data.",
            Tags = [Tags.Users])]
        public override async Task<ActionResult<IdentifierResponse>> HandleAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(r => new CreateUserCommand(r.Content.Document, r.Content.Phone))
            .Bind(command => sender.Send(command, cancellationToken))
            .Map(response => new IdentifierResponse(response.Id, tokenService.GenerateToken(response.Id.ToString())))
            .Match(Ok, this.HandleFailure);
    }
}
