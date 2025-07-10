using Core.Application.Messaging;
using Core.Application.Pagination;
using Core.Domain.Primitives;
using Core.Shared.Extensions;
using Core.Shared.Results;
using MongoDB.Driver;
using User.Domain;
using User.Persistence.Projections;
using User.Shared.Queries;
using User.Shared.Responses;

namespace User.Application.UseCases.Queries
{
    internal class PagedUserHandler(IUserProjection<Projection.User> userProjection, IUserProjection<Projection.Phone> phoneProjection, IUserProjection<Projection.Email> emailProjection) : IQueryHandler<PagedUserQuery, IPagedResult<UserResponse>>
    {
        public async Task<Result<IPagedResult<UserResponse>>> Handle(PagedUserQuery query, CancellationToken cancellationToken)
        {
            var users = await userProjection.FindPagedAsync(query.Paging, orderBy: x => x.CreatedAt, sortDirection: SortDirection.Descending, cancellationToken: cancellationToken);

            var items = await GetUsers([.. users.Items], cancellationToken);

            return Result.Success<IPagedResult<UserResponse>>(new PagedResult<UserResponse>(items, users.Page));
        }

        private async Task<List<UserResponse>> GetUsers(List<Projection.User> users, CancellationToken cancellationToken)
        {
            var response = new List<UserResponse>();

            foreach (var user in users)
            {
                var phone = await phoneProjection.FindAsync(phone => phone.UserId == user.Id, cancellationToken);
                var email = await emailProjection.FindAsync(email => email.UserId == user.Id, cancellationToken);

                response.Add(new UserResponse(
                    user.Id,
                    user?.Name,
                    user.Document,
                    phone?.Number,
                    email?.Address,
                    user?.Status,
                    user?.Address,
                    user?.DateOfBirth,
                    user.CreatedAt));
            }

            return response;
        }
    }
}
