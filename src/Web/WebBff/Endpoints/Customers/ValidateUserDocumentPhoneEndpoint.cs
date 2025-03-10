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
    public sealed class ValidateUserDocumentPhoneEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<ValidateUserDocumentPhoneRequest>
        .WithActionResult
    {
        [ApiVersion("1.0")]
        [HttpPost(UsersRoutes.ValidateUserDocumentPhone)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "ValidateUserDocumentPhone.",
            Description = "ValidateUserDocumentPhone based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.Seller)]
        public override async Task<ActionResult> HandleAsync(
        [FromBody] ValidateUserDocumentPhoneRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(validateUserDocumentPhoneRequest => new ValidateUserDocumentPhoneCommand(
                validateUserDocumentPhoneRequest.Document,
                validateUserDocumentPhoneRequest.Phone,
                validateUserDocumentPhoneRequest.EstablishmentId,
                validateUserDocumentPhoneRequest.RequestedAmount))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
