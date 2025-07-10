using Core.Application.Messaging;
using Core.Shared.Errors;
using Core.Shared.Results;
using User.Shared.Queries;
using User.Shared.Responses;
using User.Domain;
using User.Application.Errors.Validation;
using User.Persistence.Projections;

namespace User.Application.UseCases.Queries
{
    public class GetUserEmailHandler(IUserProjection<Projection.Email> emailProjection) : IQueryHandler<GetUserEmailQuery, UserEmailResponse>
    {
        public async Task<Result<UserEmailResponse>> Handle(GetUserEmailQuery query, CancellationToken cancellationToken)
        {
            var email = await emailProjection.FindAsync(email => email.UserId == query.UserId, cancellationToken);

            if (email is null)
                return Result.Failure<UserEmailResponse>(new NotFoundError(DomainError.EmailNotFound));

            return Result.Success(
                new UserEmailResponse(email.Id,
                    email.UserId,
                    email.Address,
                    email.IsConfirmed,
                    email.CreatedAt));
        }
    }
}
