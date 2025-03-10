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
    public sealed class ListEstablishmentWithOrderInProgressEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<ListEstablishmentWithOrderInProgressRequest>
        .WithActionResult<List<EstablishmentWithOrderInProgressResponse>>
    {
        [ApiVersion("1.0")]
        [HttpGet(UsersRoutes.ListEstablishmentWithOrderInProgress)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "List Establishments With Orders In Progress",
            Description = "List Establishments With Orders In Progress based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        public override async Task<ActionResult<List<EstablishmentWithOrderInProgressResponse>>> HandleAsync(
        ListEstablishmentWithOrderInProgressRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(listEstablishmentWithOrderInProgressRequest => new ListEstablishmentsWithOrdersInProgressQuery(
                ClaimsPrincipalExtensions.ExtractAuth0IdFromToken(listEstablishmentWithOrderInProgressRequest.Token)))
            .Bind(query => sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
