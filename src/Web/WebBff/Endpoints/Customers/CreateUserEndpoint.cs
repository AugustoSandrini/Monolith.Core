using Ardalis.ApiEndpoints;
using Asp.Versioning;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using User.Shared.Commands;
using User.Shared.Responses;
using WebBff.Endpoints.Customers.Requests;
using WebBff.Endpoints.Routes;

namespace WebBff.Endpoints.Customers
{
    public sealed class CreateUserEndpoint(ISender sender) : EndpointBaseAsync
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        public override async Task<ActionResult<IdentifierResponse>> HandleAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(r => new CreateUserCommand(r.Content.Document, r.Content.Phone))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
