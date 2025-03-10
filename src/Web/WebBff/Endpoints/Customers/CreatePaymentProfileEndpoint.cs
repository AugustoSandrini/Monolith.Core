using Ardalis.ApiEndpoints;
using Common.Policies;
using Core.Endpoints.Extensions;
using Core.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebBff.Endpoints.Routes;
using User.Shared.Commands;
using WebBff.Endpoints.Customers.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Asp.Versioning;

namespace WebBff.Endpoints.Customers
{
    public sealed class CreatePaymentProfileEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<CreatePaymentProfileRequest>
        .WithActionResult
    {
        [ApiVersion("1.0")]
        [HttpPost(UsersRoutes.CreatePaymentProfile)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Create a new PaymentProfile.",
            Description = "Creates a new PaymentProfile based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        public override async Task<ActionResult> HandleAsync(
        CreatePaymentProfileRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(createPaymentProfileRequest => new CreatePaymentProfileCommand(
                ClaimsPrincipalExtensions.ExtractAuth0IdFromToken(createPaymentProfileRequest.Token),
                createPaymentProfileRequest.Content.GatewayToken))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
