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

    public class GetUserPhoneHandler(IUserProjection<Projection.Phone> phoneProjection) : IQueryHandler<GetUserPhoneQuery, UserPhoneResponse>
    {
        public async Task<Result<UserPhoneResponse>> Handle(GetUserPhoneQuery query, CancellationToken cancellationToken)
        {
            var phone = await phoneProjection.FindAsync(phone => phone.UserId == query.UserId, cancellationToken);

            if (phone is null)
                return Result.Failure<UserPhoneResponse>(new NotFoundError(DomainError.PhoneNotFound));

            return Result.Success<UserPhoneResponse>(
                new(phone.Id,
                    phone.UserId,
                    phone.Number,
                    phone.IsConfirmed,
                    phone.CreatedAt));
        }
    }
}
