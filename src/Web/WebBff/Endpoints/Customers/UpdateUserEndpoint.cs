using Ardalis.ApiEndpoints;

using Core.Endpoints.Extensions;
using Core.Shared.Results;
using User.Shared.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebBff.Endpoints.Routes;
using WebBff.Endpoints.Customers.Requests;
using Asp.Versioning;

namespace WebBff.Endpoints.Customers
{
    public sealed class UpdateUserEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<UpdateUserRequest>
        .WithActionResult
    {
        [ApiVersion("1.0")]
        [HttpPut(UsersRoutes.UpdateProfile)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Update a User.",
            Description = "Update a User based on the provided request data.",
            Tags = [Tags.Users])]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult> HandleAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(updateProfileRequest => new UpdateProfileCommand(
                    updateProfileRequest.Content.Document,
                    updateProfileRequest.Content.Name,
                    updateProfileRequest.Content.Email,
                    updateProfileRequest.Content.DateOfBirth,
                    updateProfileRequest.Content.Password))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
