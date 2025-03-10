using Ardalis.ApiEndpoints;
using Common.Policies;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using WebBff.Endpoints.Routes;
using User.Shared.Commands;
using WebBff.Endpoints.Customers.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Asp.Versioning;

namespace WebBff.Endpoints.Customers
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.Seller)]
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
