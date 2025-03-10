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
    public sealed class UpdateUserNumberIntelligenceEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<UpdateUserNumberIntelligenceRequest>
        .WithActionResult
    {
        [ApiVersion("1.0")]
        [HttpPost(WebHooksRoutes.UpdateUserNumberIntelligence)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Update User number intelligence.",
            Description = "Update User number intelligence based on the provided request data.",
            Tags = [Tags.WebHooks])]
        //TODO: COLOCAR AUTENTICAÇÃO
        public override async Task<ActionResult> HandleAsync(
            [FromBody] UpdateUserNumberIntelligenceRequest request,
            CancellationToken cancellationToken = default) =>
            await Result.Create(request)
            .Map(updateNumberIntelligenceRequest => new UpdateUserNumberIntelligenceCommand(
                updateNumberIntelligenceRequest.Token,
                updateNumberIntelligenceRequest.NiAttributes?.NationalIdentityNumber?.Match))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
