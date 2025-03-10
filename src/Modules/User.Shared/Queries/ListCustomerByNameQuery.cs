using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Queries
{
    public record ListUserByNameQuery(string Name) : IQuery<List<UserResponse>>;
}
