using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Queries
{
    public sealed record ListOrderWithEstablishmentInProgressQuery(Guid UserId) : IQuery<List<OrderWithEstablishmentInProgressResponse>>;
}
