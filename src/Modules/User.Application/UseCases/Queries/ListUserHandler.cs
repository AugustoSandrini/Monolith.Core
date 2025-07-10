using Core.Application.Messaging;
using Core.Shared.Results;
using User.Domain;
using User.Persistence.Projections;
using User.Shared.Queries;
using User.Shared.Responses;

namespace User.Application.UseCases.Queries
{
    internal class ListUserHandler(IUserProjection<Projection.User> userProjection, IUserProjection<Projection.Phone> phoneProjection, IUserProjection<Projection.Email> emailProjection) : IQueryHandler<ListUserQuery, List<UserResponse>>
    {
        public async Task<Result<List<UserResponse>>> Handle(ListUserQuery request, CancellationToken cancellationToken)
        {
            var users = await userProjection.ListAsync(cancellationToken);

            var response = new List<UserResponse>();

            if (users is null || users.Count == 0)
                return Result.Success(response);

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
                    user.Status,
                    user?.Address,
                    user?.DateOfBirth,
                    user.CreatedAt));
            }

            return Result.Success(response);
        }
    }
}
