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
        IUserProjection<Projection.Email> emailProjection,
        IUserProjection<Projection.CreditConsultation> creditConsultationProjection,
        IUserProjection<Projection.PaymentProfile> paymentProfileProjection) : IQueryHandler<GetUserByDocumentQuery, UserResponse>
    {
        public async Task<Result<UserResponse>> Handle(GetUserByDocumentQuery query, CancellationToken cancellationToken)
        {
            var User = await UserProjection.FindAsync(User => User.Document.Contains(query.Document.RemoveNonAlphaNumericCharacters()), cancellationToken);

            if (User is null)
                return Result.Failure<UserResponse>(new NotFoundError(DomainError.UserNotFound));

            var phoneTask = phoneProjection.FindAsync(phone => phone.UserId == User.Id, cancellationToken);
            var emailTask = emailProjection.FindAsync(email => email.UserId == User.Id, cancellationToken);
            var creditConsultationsTask = creditConsultationProjection.ListAsync(creditConsultation => creditConsultation.UserId == User.Id, cancellationToken);
            var paymentProfilesTask = paymentProfileProjection.ListAsync(paymentProfile => paymentProfile.UserId == User.Id, cancellationToken);

            await Task.WhenAll(phoneTask, emailTask, creditConsultationsTask, paymentProfilesTask);

            var phone = await phoneTask;
            var email = await emailTask;
            var creditConsultations = await creditConsultationsTask;
            var paymentProfiles = await paymentProfilesTask;

            return Result.Success<UserResponse>(
                new(User.Id,
                    User.Name,
                    User.Document,
                    phone.Number,
                    email?.Address,
                    User.Status,
                    User.Address,
                    User.DateOfBirth,
                    User.LastTokenSentAt,
                    User.VindiExternalId,
                    paymentProfiles is not null ? paymentProfiles.Select(pp => (Dto.PaymentProfile)pp).ToList()  : [],
                    creditConsultations is not null ? creditConsultations.Select(cc => (Dto.CreditConsultation)cc).ToList() : [],
                    User.CreatedAt));
        }
    }
}
