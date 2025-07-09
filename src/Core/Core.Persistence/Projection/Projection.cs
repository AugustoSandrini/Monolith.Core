﻿using Core.Application.Pagination;
using Core.Domain.Primitives;
using Core.Domain.Projection;
using Core.Persistence.Projection.Abstractions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Core.Persistence.Projection
{
    public class Projection<TProjection>(IMongoDbContext context) : IProjection<TProjection>
    where TProjection : Domain.Primitives.IProjection
    {
        private readonly IMongoCollection<TProjection> _collection = context.GetCollection<TProjection>();

        public Task<TProjection> GetAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : struct
            => FindAsync(projection => projection.Id.Equals(id), cancellationToken);

        public Task<TProjection> FindAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken = default)
            => _collection.AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken)!;

        public Task<List<TProjection>> ListAsync(CancellationToken cancellationToken = default)
            => _collection.AsQueryable().ToListAsync(cancellationToken: cancellationToken);

        public Task<List<TProjection>> ListAsync(Expression<Func<TProjection, bool>> predicate, CancellationToken cancellationToken = default)
            => _collection.AsQueryable().Where(predicate).ToListAsync(cancellationToken: cancellationToken);

        public Task<IPagedResult<TProjection>> FindPagedAsync(
            Expression<Func<TProjection, bool>> predicate,
            Paging paging,
            Expression<Func<TProjection, object>>? orderBy = null,
            SortDirection sortDirection = SortDirection.Ascending,
            CancellationToken cancellationToken = default)
        {
            return FindPagedAsync<TProjection>(predicate, paging, orderBy, sortDirection, null, cancellationToken);
        }

        public async Task<IPagedResult<TDestination>> FindPagedAsync<TDestination>(
            Expression<Func<TProjection, bool>> predicate,
            Paging paging,
            Expression<Func<TProjection, object>>? orderBy = null,
            SortDirection sortDirection = SortDirection.Ascending,
            Expression<Func<TProjection, TDestination>>? projection = null,
            CancellationToken cancellationToken = default)
        {
            var skip = (paging.Number - 1) * paging.Size;
            var queryable = _collection.AsQueryable().Where(predicate);
            var total = await queryable.CountAsync(cancellationToken);

            if (orderBy is not null)
            {
                queryable = sortDirection == SortDirection.Descending
                    ? queryable.OrderByDescending(orderBy)
                    : queryable.OrderBy(orderBy);
            }

            IQueryable<TDestination> query;

            if (projection is not null)
                query = queryable.Select(projection);
            else if (typeof(TDestination) == typeof(TProjection))
                query = (IQueryable<TDestination>)queryable;
            else
                throw new InvalidOperationException($"Projection is null, but destination type '{typeof(TDestination).Name}' differs from projection type '{typeof(TProjection).Name}'.");

            var items = await query
                .Skip(skip)
                .Take(paging.Size)
                .ToListAsync(cancellationToken);

            var page = new Page(
                HasPrevious: paging.Number > 1,
                HasNext: total > paging.Number * paging.Size,
                Number: paging.Number,
                Size: paging.Size,
                Total: total
            );

            return new PagedResult<TDestination>(items, page);
        }

        public Task DeleteAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : struct
            => _collection.DeleteOneAsync(projection => projection.Id.Equals(id), cancellationToken);

        public Task DeleteAsync(Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken = default)
            => _collection.DeleteManyAsync(filter, cancellationToken);

        public Task UpdateOneFieldAsync<TField>(Expression<Func<TProjection, bool>> filter, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken = default)
            => _collection.UpdateOneAsync(
                filter: filter,
                update: new ObjectUpdateDefinition<TProjection>(new()).Set(field, value),
                cancellationToken: cancellationToken);

        public Task UpdateOneFieldAsync<TField, TId>(TId id, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken = default) where TId : struct
            => _collection.UpdateOneAsync(
                filter: projection => projection.Id.Equals(id),
                update: new ObjectUpdateDefinition<TProjection>(new()).Set(field, value),
                cancellationToken: cancellationToken);

        public Task UpdateManyFieldAsync<TField>(Expression<Func<TProjection, bool>> filter, Expression<Func<TProjection, TField>> field, TField value, CancellationToken cancellationToken = default)
            => _collection.UpdateManyAsync(
                filter: filter,
                update: new ObjectUpdateDefinition<TProjection>(new()).Set(field, value),
                cancellationToken: cancellationToken);

        public ValueTask ReplaceInsertAsync(TProjection replacement, CancellationToken cancellationToken = default)
            => OnReplaceAsync(replacement, projection => projection.Id == replacement.Id, cancellationToken);

        public ValueTask ReplaceInsertAsync(TProjection replacement, Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken = default)
            => OnReplaceAsync(replacement, filter, cancellationToken);

        public ValueTask RebuildInsertAsync(TProjection replacement, CancellationToken cancellationToken = default)
            => OnReplaceAsync(replacement, projection => projection.Id == replacement.Id, cancellationToken);

        public ValueTask RebuildInsertAsync(TProjection replacement, Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken = default)
            => OnReplaceAsync(replacement, filter, cancellationToken);

        private async ValueTask OnReplaceAsync(TProjection replacement, Expression<Func<TProjection, bool>> filter, CancellationToken cancellationToken = default)
            => await _collection.ReplaceOneAsync(filter, replacement, new ReplaceOptions { IsUpsert = true }, cancellationToken);

        public IMongoCollection<TProjection> GetCollection()
                => _collection;
    }
}
