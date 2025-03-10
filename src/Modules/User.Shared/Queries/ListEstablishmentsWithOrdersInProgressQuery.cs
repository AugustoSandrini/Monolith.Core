using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Queries
{
    public sealed record ListEstablishmentsWithOrdersInProgressQuery(Guid UserId) : IQuery<List<EstablishmentWithOrderInProgressResponse>>;
}
