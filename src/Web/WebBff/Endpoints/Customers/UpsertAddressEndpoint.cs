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
    public sealed class UpsertAddressEndpoint(ISender sender) : EndpointBaseAsync
        .WithRequest<UpsertAddressRequest>
        .WithActionResult
    {
        [ApiVersion("1.0")]
        [HttpPut(UsersRoutes.UpsertAddress)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Upsert a Address.",
            Description = "Upsert a Address based on the provided request data.",
            Tags = [Tags.Users])]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = Policies.User)]
        public override async Task<ActionResult> HandleAsync(
        UpsertAddressRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(upsertAddressRequest => new UpsertAddressCommand(
                upsertAddressRequest.UserId,
                upsertAddressRequest.Content.Address))
            .Bind(command => sender.Send(command, cancellationToken))
            .Match(Ok, this.HandleFailure);
    }
}
