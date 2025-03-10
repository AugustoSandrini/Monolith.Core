using Core.Application.Messaging;
using Core.Shared.Results;
using User.Domain;
using User.Domain.Enumerations;

namespace User.Application.UseCases.Commands
{
    using User.Application.Services;
    using User.Domain.Aggregates;
    using User.Persistence.Projections;
    using MongoDB.Driver;

    public record ExpireCreditConsultationCommand : ICommand;

    internal class ExpireCreditConsultationHandler(
        IUserApplicationService applicationService,
        IUserProjection<Projection.CreditConsultation> projectionCreditConsultation) : ICommandHandler<ExpireCreditConsultationCommand>
    {
        public async Task<Result> Handle(ExpireCreditConsultationCommand cmd, CancellationToken cancellationToken)
        {
            var startOfDay = DateTimeOffset.Now.Date;
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);

            var creditConsultations = await projectionCreditConsultation.ListAsync(x =>
                x.Status.Equals(CreditConsultationStatus.Accepted) &&
                x.ExpireAt >= startOfDay &&
                x.ExpireAt <= endOfDay,
                cancellationToken);

            foreach (var creditConsultation in creditConsultations)
            {
                var UserResult = await applicationService.LoadAggregateAsync<User>(creditConsultation.UserId, cancellationToken);

                if (UserResult.IsFailure)
                    continue;

                var User = UserResult.Value;

                User.ExpireCreditConsultation(creditConsultation.Id, creditConsultation.OrderId);

                await applicationService.AppendEventsAsync(User, cancellationToken);
            }

            return Result.Success();
        }
    }
}
