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
    public sealed class GetCreditConsultationByOrderIdEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<GetCreditConsultationByOrderIdRequest>
        .WithActionResult<CreditConsultationResponse>
    {
        [ApiVersion("1.0")]
        [HttpGet(UsersRoutes.GetCreditConsultationByOrderId)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Get CreditConsultation By OrderId",
            Description = "Get CreditConsultation By OrderId based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        public override async Task<ActionResult<CreditConsultationResponse>> HandleAsync(
            GetCreditConsultationByOrderIdRequest request,
            CancellationToken cancellationToken = default) =>
            await Result.Create(request)
            .Map(getCreditConsultationByOrderIdRequest => new GetCreditConsultationByOrderIdQuery(
                ClaimsPrincipalExtensions.ExtractAuth0IdFromToken(getCreditConsultationByOrderIdRequest.Token),
                getCreditConsultationByOrderIdRequest.OrderId))
            .Bind(query => sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
