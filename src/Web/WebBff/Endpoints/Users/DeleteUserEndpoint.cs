using Ardalis.ApiEndpoints;
using Common.Policies;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using WebBff.Endpoints.Routes;
using User.Shared.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Asp.Versioning;
using WebBff.Endpoints.Users.Requests;

namespace WebBff.Endpoints.Users
{
    public sealed class DeleteUserEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<DeleteUserRequest>
        .WithActionResult
    {
        [ApiVersion("1.0")]
        [HttpDelete(UsersRoutes.DeleteUser)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Delete a User.",
            Description = "Delete a User based on the provided request data.",
            Tags = [Tags.Users])]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<ActionResult> HandleAsync(
        DeleteUserRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(deleteUserRequest => new DeleteUserCommand(
                deleteUserRequest.UserId))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(NoContent, this.HandleFailure);
    }
}
