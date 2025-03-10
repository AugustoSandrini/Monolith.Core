using Core.Application.Messaging;
using Core.Shared.Results;
using User.Application.Errors.Validation;
using User.Application.Services;
using User.Domain.Enumerations;
using User.Shared.Commands;
using User.Shared.Responses;
using MediatR;
using Serilog;

namespace User.Application.UseCases.Commands
{
    using Domain.Aggregates;
    using static User.Domain.Enumerations.UserStatus;

    public class CreatePaymentProfileHandler(IUserApplicationService applicationService,
        //IVindiClient vindiClient,
        ISender sender,
        ILogger logger) : ICommandHandler<CreatePaymentProfileCommand, IdentifierResponse>
    {
        public async Task<Result<IdentifierResponse>> Handle(CreatePaymentProfileCommand cmd, CancellationToken cancellationToken)
        {
            //try
            //{
            //    var UserResult = await applicationService.LoadAggregateAsync<User>(cmd.UserId, cancellationToken);

            //    if (UserResult.IsFailure)
            //        return Result.Failure<IdentifierResponse>(UserResult.Error);

            //    var User = UserResult.Value;

            //var vindiResponse = await vindiClient.IncludePaymentProfile(new()
            //{
            //    GatewayToken = cmd.GatewayToken,
            //    UserId = User.VindiExternalId
            //},
            //cancellationToken);

            //    switch (User.Status)
            //    {
            //        case DefaulterStatus:
            //            return await ProcessDebtSettlement(sender, cmd, User, vindiResponse, cancellationToken);
            //        case SaleInProgressStatus:
            //            var verifyResult = await VerifyPaymentProfile(vindiResponse.PaymentProfile.Id, cmd, User, cancellationToken);

            //            if (verifyResult.IsFailure)
            //                return Result.Failure<IdentifierResponse>(verifyResult.Error);

            //            return verifyResult;
            //        default:
            //            return await SavePaymentProfile(cmd, vindiResponse.PaymentProfile.Id, User, cancellationToken);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex, $"Falha ao incluir perfil de pagamento: {cmd.GatewayToken} na Vindi para o Cliente: {cmd.UserId}.");

                return Result.Failure<IdentifierResponse>(DomainError.PaymentMethodCouldntBeCreated);
            //}
        }

        //private async Task<Result<IdentifierResponse>> ProcessDebtSettlement(ISender sender, CreatePaymentProfileCommand cmd, User User, Twila.Vindi.Services.Results.IncludePaymentProfileResult vindiResponse, CancellationToken cancellationToken)
        //{
        //    var overdueBillsResult = await sender.Send(new ListFrozenOrderBillsByUserIdQuery(cmd.UserId), cancellationToken);

        //    if (overdueBillsResult.IsFailure)
        //        return Result.Failure<IdentifierResponse>(overdueBillsResult.Error);

        //    var overdueBills = overdueBillsResult.Value;

        //    var paymentIdResult = await sender.Send(
        //        new CreateDebtSettlementCommand(overdueBills.OrderId,
        //        overdueBills.DebtsIds,
        //        vindiResponse.PaymentProfile.Id,
        //        overdueBills.TotalAmount
        //        ));

        //    if (paymentIdResult.IsFailure)
        //    {
        //        await DeletePaymentProfile(vindiResponse.PaymentProfile.Id, cancellationToken);
        //        return Result.Failure<IdentifierResponse>(paymentIdResult.Error);
        //    }

        //    var result = await SavePaymentProfile(cmd, vindiResponse.PaymentProfile.Id, User, cancellationToken);

        //    if (result.IsFailure)
        //        return Result.Failure<IdentifierResponse>(result.Error);

        //    return result;
        //}

        //private async Task<Result<IdentifierResponse>> VerifyPaymentProfile(int paymentProfileId, CreatePaymentProfileCommand cmd, User User, CancellationToken cancellationToken)
        //{
        //    var vindiValidateResponse = await vindiClient.VerifyPaymentProfile(
        //        paymentProfileId,
        //        cancellationToken);

        //    if (vindiValidateResponse.Transaction.Status.Equals(PaymentProfileStatus.Success, StringComparison.OrdinalIgnoreCase))
        //        return await SavePaymentProfile(cmd, paymentProfileId, User, cancellationToken);

        //    await DeletePaymentProfile(paymentProfileId, cancellationToken);

        //    return Result.Failure<IdentifierResponse>(DomainError.PaymentMethodCouldntBeVerified);
        //}

        //private async Task DeletePaymentProfile(int paymentProfileId, CancellationToken cancellationToken)
        //{
        //    await vindiClient.DeletePaymentProfile(
        //                    paymentProfileId,
        //                    cancellationToken);
        //}

        private async Task<Result<IdentifierResponse>> SavePaymentProfile(CreatePaymentProfileCommand cmd, int externalId, User User, CancellationToken cancellationToken)
        {
            var paymentProfileId = User
                                    .CreatePaymentProfile(cmd.GatewayToken, externalId);

            await applicationService.AppendEventsAsync(User, cancellationToken);

            return Result.Success(new IdentifierResponse(paymentProfileId));
        }
    }
}
