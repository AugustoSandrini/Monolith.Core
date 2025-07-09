using Ardalis.ApiEndpoints;
using Common.Policies;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using User.Shared.Queries;
using User.Shared.Responses;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebBff.Endpoints.Routes;
using WebBff.Endpoints.Customers.Requests;
using Asp.Versioning;

namespace WebBff.Endpoints.Customers
{
    public sealed class GetUserByIdEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<GetUserByIdRequest>
        .WithActionResult<UserResponse>
    {
        [ApiVersion("1.0")]
        [HttpGet(UsersRoutes.GetUserById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Get User By UserId",
            Description = "Get User By UserId based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        public override async Task<ActionResult<UserResponse>> HandleAsync(
        GetUserByIdRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(getUserDetailRequest => new GetUserByIdQuery(getUserDetailRequest.UserId))
            .Bind(query => sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
