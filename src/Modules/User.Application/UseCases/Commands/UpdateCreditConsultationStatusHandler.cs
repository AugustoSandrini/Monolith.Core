using Core.Application.Messaging;
using Core.Shared.Results;
using User.Domain.Enumerations;
using MongoDB.Driver;

namespace User.Application.UseCases.Commands
{
    using User.Application.Services;
    using User.Domain.Aggregates;
    using User.Shared.Commands;

    public class UpdateCreditConsultationStatusHandler(
        IUserApplicationService applicationService) : ICommandHandler<UpdateCreditConsultationStatusCommand>
    {
        public async Task<Result> Handle(UpdateCreditConsultationStatusCommand cmd, CancellationToken cancellationToken)
        {
            var UserResult = await applicationService.LoadAggregateAsync<User>(cmd.UserId, cancellationToken);

            if (UserResult.IsFailure)
                return Result.Failure(UserResult.Error);

            var User = UserResult.Value;

            var creditConsultationId = User.CreditConsultations.First(cc =>
                cc.OrderId == cmd.OrderId &&
                cc.Status != CreditConsultationStatus.Expired).Id;

            ChangeStatus(User, cmd.OrderId, cmd.DecisionIp, creditConsultationId, cmd.Status);

            await applicationService.AppendEventsAsync(User, cancellationToken);

            return Result.Success();
        }

        private void ChangeStatus(User User, Guid orderId, string decisionIp, Guid creditConsultationId, CreditConsultationStatus status)
        {
            switch (status)
            {
                case CreditConsultationStatus.AcceptedStatus:
                    User.AcceptCreditConsultation(creditConsultationId, orderId, decisionIp);
                    break;
                case CreditConsultationStatus.RefusedStatus:
                    User.RefuseCreditConsultation(creditConsultationId, orderId, decisionIp);
                    break;
            }
        }
    }
}
