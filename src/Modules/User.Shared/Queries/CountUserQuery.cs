using Core.Application.Messaging;

namespace User.Shared.Queries
{
    public sealed record CountUserQuery(DateTime? StartDate, DateTime? EndDate) : IQuery<long>;
}
