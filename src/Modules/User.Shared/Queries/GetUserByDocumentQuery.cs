using Core.Application.Messaging;
using User.Shared.Responses;

namespace User.Shared.Queries
{
    public record GetUserByDocumentQuery(string Document) : IQuery<UserResponse>;
}
