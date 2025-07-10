using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Queries
{
    public record ListUserQuery : IQuery<List<UserResponse>>;
}
