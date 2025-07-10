using Core.Application.Messaging;
using Core.Shared.Errors;
using Core.Shared.Results;
using User.Application.Errors.Validation;
using User.Application.Extensions;
using User.Domain;
using User.Persistence.Projections;
using User.Shared.Queries;
using User.Shared.Responses;

namespace User.Application.UseCases.Queries
{
    public class GetUserByDocumentHandler(
        IUserProjection<Projection.User> UserProjection,
        IUserProjection<Projection.Phone> phoneProjection,
        IUserProjection<Projection.Email> emailProjection) : IQueryHandler<GetUserByDocumentQuery, UserResponse>
    {
        public async Task<Result<UserResponse>> Handle(GetUserByDocumentQuery query, CancellationToken cancellationToken)
        {
            var user = await UserProjection.FindAsync(user => user.Document.Contains(query.Document.RemoveNonAlphaNumericCharacters()), cancellationToken);

            if (user is null)
                return Result.Failure<UserResponse>(new NotFoundError(DomainError.UserNotFound));

            var phoneTask = phoneProjection.FindAsync(phone => phone.UserId == user.Id, cancellationToken);
            var emailTask = emailProjection.FindAsync(email => email.UserId == user.Id, cancellationToken);

            await Task.WhenAll(phoneTask, emailTask);

            var phone = await phoneTask;
            var email = await emailTask;

            return Result.Success(new UserResponse(
                user.Id,
                user.Name,
                user.Document,
                phone.Number,
                email?.Address,
                user.Status,
                user.Address,
                user.DateOfBirth,
                user.CreatedAt));
        }
    }
}
