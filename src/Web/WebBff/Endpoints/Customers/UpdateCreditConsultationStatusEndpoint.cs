using Ardalis.ApiEndpoints;

using Common.Policies;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using User.Shared.Commands;
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
    public sealed class UpdateCreditConsultationStatusEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<UpdateCreditConsultationStatusRequest>
        .WithActionResult
    {
        [ApiVersion("1.0")]
        [HttpPost(UsersRoutes.UpdateCreditConsultationStatus)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Update CreditConsultation status.",
            Description = "Update CreditConsultation status based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        public override async Task<ActionResult> HandleAsync(
        UpdateCreditConsultationStatusRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(updateCreditConsultationStatusRequest => new UpdateCreditConsultationStatusCommand(
                ClaimsPrincipalExtensions.ExtractAuth0IdFromToken(updateCreditConsultationStatusRequest.Token),
                updateCreditConsultationStatusRequest.Content.OrderId,
                "DecisionIp",
                updateCreditConsultationStatusRequest.Content.Accepted))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
