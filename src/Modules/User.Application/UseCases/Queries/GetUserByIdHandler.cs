using Core.Application.Messaging;
using Core.Shared.Errors;
using Core.Shared.Results;
using User.Application.Errors.Validation;
using User.Domain;
using User.Persistence.Projections;
using User.Shared.Queries;
using User.Shared.Responses;

namespace User.Application.UseCases.Queries
{
    public class GetUserByIdHandler(
        IUserProjection<Projection.User> userProjection,
        IUserProjection<Projection.Phone> phoneProjection,
        IUserProjection<Projection.Email> emailProjection) : IQueryHandler<GetUserByIdQuery, UserResponse>
    {
        public async Task<Result<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var user = await userProjection.FindAsync(user => user.Id == query.UserId, cancellationToken);

            if (user is null)
                return Result.Failure<UserResponse>(new NotFoundError(DomainError.UserNotFound));

            var phoneTask = phoneProjection.FindAsync(phone => phone.UserId == user.Id, cancellationToken);
            var emailTask = emailProjection.FindAsync(email => email.UserId == user.Id, cancellationToken);

            await Task.WhenAll(phoneTask, emailTask);

            var phone = await phoneTask;
            var email = await emailTask;

            return Result.Success(
                new UserResponse(user.Id,
                    user.Name,
                    user.Document,
                    phone?.Number,
                    email?.Address,
                    user.Status,
                    user.Address,
                    user.DateOfBirth,
                    user.CreatedAt));
        }
    }
}
