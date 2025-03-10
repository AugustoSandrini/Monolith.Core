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
    public sealed class ListOrderWithEstablishmentInProgressEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<ListOrderWithEstablishmentInProgressRequest>
        .WithActionResult<List<OrderWithEstablishmentInProgressResponse>>
    {
        [ApiVersion("1.0")]
        [HttpGet(UsersRoutes.ListOrderWithEstablishmentInProgress)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "List Orders With Establishments In Progress",
            Description = "List Orders With Establishments In Progress based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        public override async Task<ActionResult<List<OrderWithEstablishmentInProgressResponse>>> HandleAsync(
        ListOrderWithEstablishmentInProgressRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(listOrderWithEstablishmentInProgressRequest => new ListOrderWithEstablishmentInProgressQuery(
                ClaimsPrincipalExtensions.ExtractAuth0IdFromToken(listOrderWithEstablishmentInProgressRequest.Token)))
            .Bind(query => sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
