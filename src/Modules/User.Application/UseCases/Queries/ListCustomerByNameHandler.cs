using Core.Application.Messaging;
using Core.Shared.Errors;
using Core.Shared.Results;
using System.Runtime.CompilerServices;
using User.Domain;
using User.Shared.Queries;
using User.Shared.Responses;
using User.Application.Errors.Validation;
using User.Persistence.Projections;
using MongoDB.Bson;
using MongoDB.Driver;

namespace User.Application.UseCases.Queries
{
    public class ListUserByNameHandler(
        IUserProjection<Projection.User> UserProjection,
        IUserProjection<Projection.Phone> phoneProjection,
        IUserProjection<Projection.Email> emailProjection,
        IUserProjection<Projection.CreditConsultation> creditConsultationProjection,
        IUserProjection<Projection.PaymentProfile> paymentProfileProjection) : IQueryHandler<ListUserByNameQuery, List<UserResponse>>
    {
        public async Task<Result<List<UserResponse>>> Handle(ListUserByNameQuery query, CancellationToken cancellationToken)
        {
            var UserCollection = UserProjection.GetCollection();

            var UserFilter = Builders<Projection.User>.Filter.Empty;

            UserFilter = Builders<Projection.User>.Filter.Regex(User => User.Name, new BsonRegularExpression(query.Name, "i"));

            var Users = await UserCollection
               .Find(UserFilter)
               .ToListAsync(cancellationToken);

            if (Users is null || Users.Count == 0)
                return Result.Failure<List<UserResponse>>(new NotFoundError(DomainError.UserNotFound));

            var responses = new List<UserResponse>();

            await foreach (var response in StreamUserByNameResponsesAsync(Users, cancellationToken))
                responses.Add(response);

            return Result.Success(responses);
        }

        private async IAsyncEnumerable<UserResponse> StreamUserByNameResponsesAsync(
            List<Projection.User> Users,
            [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            foreach (var User in Users)
            {
                var phoneTask = phoneProjection.FindAsync(phone => phone.UserId == User.Id, cancellationToken);
                var emailTask = emailProjection.FindAsync(email => email.UserId == User.Id, cancellationToken);
                var creditConsultationsTask = creditConsultationProjection.ListAsync(creditConsultation => creditConsultation.UserId == User.Id, cancellationToken);
                var paymentProfilesTask = paymentProfileProjection.ListAsync(paymentProfile => paymentProfile.UserId == User.Id, cancellationToken);

                await Task.WhenAll(phoneTask, emailTask, creditConsultationsTask, paymentProfilesTask);

                var phone = await phoneTask;
                var email = await emailTask;
                var creditConsultations = await creditConsultationsTask;
                var paymentProfiles = await paymentProfilesTask;

                yield return new UserResponse(
                    User.Id,
                    User.Name,
                    User.Document,
                    phone?.Number,
                    email?.Address,
                    User.Status,
                    User.Address,
                    User.DateOfBirth,
                    User.LastTokenSentAt,
                    User.VindiExternalId,
                    paymentProfiles?.Select(pp => (Dto.PaymentProfile)pp).ToList() ?? [],
                    creditConsultations?.Select(cc => (Dto.CreditConsultation)cc).ToList() ?? [],
                    User.CreatedAt);
            }
        }
    }
}
