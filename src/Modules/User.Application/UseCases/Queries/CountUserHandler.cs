using Core.Application.Messaging;
using Core.Shared.Results;
using User.Domain;
using User.Persistence.Projections;
using User.Shared.Queries;
using MongoDB.Driver;

namespace User.Application.UseCases.Queries
{
    public class CountUserHandler(
        IUserProjection<Projection.User> UserProjection) : IQueryHandler<CountUserQuery, long>
    {
        public async Task<Result<long>> Handle(CountUserQuery query, CancellationToken cancellationToken)
        {
            var collection = UserProjection.GetCollection();

            var filter = Builders<Projection.User>.Filter.Empty;

            if (query.StartDate.HasValue)
                filter &= Builders<Projection.User>.Filter.Gte(c => c.CreatedAt.Date, query.StartDate.Value.Date);

            if (query.EndDate.HasValue)
                filter &= Builders<Projection.User>.Filter.Lte(c => c.CreatedAt.Date, query.EndDate.Value.Date);

            var Users = await collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

            return Result.Success(Users);
        }
    }
}
