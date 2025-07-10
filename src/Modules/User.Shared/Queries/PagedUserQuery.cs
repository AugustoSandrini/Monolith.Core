using Core.Application.Messaging;
using Core.Domain.Primitives;
using User.Shared.Responses;

namespace User.Shared.Queries
{
    public sealed record PagedUserQuery(Paging Paging) : IQuery<IPagedResult<UserResponse>>;
}
