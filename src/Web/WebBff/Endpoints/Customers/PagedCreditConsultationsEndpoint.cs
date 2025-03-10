using Ardalis.ApiEndpoints;
using Common.Policies;
using Core.Domain.Primitives;
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
    public sealed class PagedCreditConsultationsEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<PagedCreditConsultationsRequest>
        .WithActionResult<IPagedResult<CreditConsultationResponse>>
    {
        [ApiVersion("1.0")]
        [HttpGet(UsersRoutes.PagedCreditConsultations)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Paged CreditConsultations",
            Description = "Paged CreditConsultations based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        public override async Task<ActionResult<IPagedResult<CreditConsultationResponse>>> HandleAsync(
            PagedCreditConsultationsRequest request,
            CancellationToken cancellationToken = default) =>
            await Result.Create(request)
            .Map(pagedCreditConsultationsRequest => new PagedCreditConsultationsQuery(
                ClaimsPrincipalExtensions.ExtractAuth0IdFromToken(pagedCreditConsultationsRequest.Token),
                new(pagedCreditConsultationsRequest.Limit, pagedCreditConsultationsRequest.Offset)))
            .Bind(query => sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
