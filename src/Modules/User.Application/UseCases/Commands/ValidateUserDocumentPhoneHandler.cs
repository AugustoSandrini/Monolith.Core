using Core.Application.Messaging;
using Core.Shared.Results;
using User.Application.Extensions;
using User.Domain.Enumerations;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;


namespace User.Application.UseCases.Commands
{
    using Core.Shared.Errors;
    using User.Application.Errors.Validation;
    using User.Application.Services;
    using User.Domain;
    using User.Persistence.Projections;
    using User.Shared.Commands;
    using MediatR;
    using static User.Domain.Event;

    public class ValidateUserDocumentPhoneHandler(
        IUserApplicationService applicationService,
        IUserProjection<Projection.User> UserProjection,
        IUserProjection<Projection.Phone> UserPhoneProjection,
        ISender sender) : ICommandHandler<ValidateUserDocumentPhoneCommand>
    //IClaroClient claroClient,
    //IOptionsSnapshot<ClaroClientOptions> claroClientOptions,
    //CreateNumberIntelligenceInteractor createNumberIntelligenceInteractor,
    //GetNumberIntelligenceInteractor getNumberIntelligenceInteractor) : ICommandHandler<ValidateUserDocumentPhoneCommand>
    {
        //TODO: Validar valor do pedido dentro do limite do que o estabelecimentoChain aceita
        public async Task<Result> Handle(ValidateUserDocumentPhoneCommand cmd, CancellationToken cancellationToken)
        {
            var UserId = Guid.Empty;

            var UserValidationResult = await ValidateUserExistsAsync(cmd.Document.RemoveNonAlphaNumericCharacters(), cmd.Phone.RemoveSpecialCharacters(), UserId, cancellationToken);

            if(UserValidationResult.IsFailure)
                return Result.Failure(UserValidationResult.Error);

            UserId = UserValidationResult.Value;

            //var establishmentResult = await sender.Send(new GetEstablishmentByIdQuery(cmd.EstablishmentId), cancellationToken);

            //if (establishmentResult.IsFailure)
            //    return Result.Failure(new NotFoundError(DomainError.EstablishmentNotFound));

            //var establishment = establishmentResult.Value;

            //var establishmentChainResult = await sender.Send(new GetEstablishmentChainByIdQuery(establishment.EstablishmentChainId), cancellationToken);

            //if (establishmentChainResult.IsFailure)
            //    return Result.Failure(new NotFoundError(DomainError.EstablishmentChainNotFound));

            //var establishmentChain = establishmentChainResult.Value;

            //var payingFundResult = await sender.Send(new GetPayingFundByIdQuery(establishmentChain.PayingFundId), cancellationToken);

            //if (payingFundResult.IsFailure)
            //    return Result.Failure(new NotFoundError(DomainError.PayingFundNotFound));

            //var payingFund = payingFundResult.Value;

            //if (cmd.RequestedAmount > payingFund.MaximumAmount || cmd.RequestedAmount < payingFund.MinimumAmount)
            //    return Result.Failure(DomainError.RequestedAmountInvalid);

            //var numberIntelligenceResult = await ValidateNumberIntelligenceAsync(cmd.Document.RemoveNonAlphaNumericCharacters(), cmd.Phone.RemoveSpecialCharacters(), cancellationToken);

            //if (numberIntelligenceResult.IsFailure)
            //    return Result.Failure(numberIntelligenceResult.Error);

            //await applicationService.PublishEventAsync(new UserDocumentAndPhoneValidated(UserId, cmd.Document.RemoveNonAlphaNumericCharacters(), cmd.Phone.RemoveSpecialCharacters(), cmd.EstablishmentId, cmd.RequestedAmount), cancellationToken);

            return Result.Success();
        }

        private async Task<Result<Guid>> ValidateUserExistsAsync(string document, string phone, Guid UserId, CancellationToken cancellationToken)
        {
            var User = await UserProjection.FindAsync(x => x.Document == document, cancellationToken);
            
            if (User is not null)
            {
                if (User.Status != UserStatus.Active && User.Status != UserStatus.PendingProfile)
                    return Result.Failure<Guid>(DomainError.UserBlocked);

                var sameDocument = User.Document.Equals(document, StringComparison.OrdinalIgnoreCase);

                var UserPhone = await UserPhoneProjection.FindAsync(p => p.UserId == User.Id, cancellationToken);

                var samePhone = UserPhone.Number.Equals(phone, StringComparison.OrdinalIgnoreCase);

                if (sameDocument && samePhone)
                    return Result.Success(User.Id);
            
                return Result.Failure<Guid>(DomainError.AlreadyRegisteredDocument);
            }

            var phoneExist = await UserPhoneProjection.FindAsync(x => x.Number == phone, cancellationToken);
            
            if (phoneExist is not null)
                return Result.Failure<Guid>(DomainError.AlreadyRegisteredNumber);

            return Result.Success(UserId);
        }

        //private async Task<Result> ValidateNumberIntelligenceAsync(string document, string phone, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        //var numberIntelligence = await getNumberIntelligenceInteractor.Handle(new(ni => ni.Document == document && ni.Phone == phone), cancellationToken);

        //        if (numberIntelligence is not null)
        //        {
        //            if (numberIntelligence.Status == NumberIntelligenceStatus.Unmatched)
        //                return Result.Failure(DomainError.PhoneNotOwned);

        //            if (numberIntelligence.Status == NumberIntelligenceStatus.Pending)
        //            {
        //                var pendingPooledResult = await NumberIntelligenceStatusPoolingAsync(document, phone, cancellationToken);

        //                if (pendingPooledResult.IsFailure)
        //                    return Result.Failure(pendingPooledResult.Error);
        //            }

        //            return Result.Success();
        //        }

        //        //var validationTokenResult = await CallIntelligenceNumber(phone, document, cancellationToken);

        //        if (validationTokenResult.IsFailure)
        //            return Result.Failure(validationTokenResult.Error);

        //        var token = validationTokenResult.Value;

        //        await createNumberIntelligenceInteractor.Handle(new(token, phone, document), cancellationToken);

        //        var pooled = await NumberIntelligenceStatusPoolingAsync(document, phone, cancellationToken);

        //        if (pooled.IsFailure)
        //            return Result.Failure(pooled.Error);

        //        return Result.Success();
        //    }
        //    catch (Exception ex) 
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return Result.Success();
        //}

        //private async Task<Result<string>> CallIntelligenceNumber(string phone, string document, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        var numberIntelligence = await claroClient.NumberIntelligenceAsync(new()
        //        {
        //            ConsentGranted = true,
        //            PhoneNumber = phone.RemoveSpecialCharacters(),
        //            CallbackUrl = claroClientOptions.Value.CallBackUrl,
        //            Attributes = new()
        //            {
        //                NationalIdentityNumber = new() { NationalId = document },
        //                SimSwap = new() { Period = claroClientOptions.Value.SimSwapPeriod }
        //            }
        //        }, cancellationToken);

        //        return Result.Success(numberIntelligence.Token);
        //    }
        //    catch (ApiException ex)
        //    {
        //        Log.Error(ex, $"Erro ao tentar validar documento: {document} com o telefone: {phone}");

        //        if (ex.StatusCode == HttpStatusCode.BadRequest)
        //        {
        //            if (ex.Content?.Contains("EC_NO_COVERAGE") == true || ex.Message.Contains("EC_NO_COVERAGE"))
        //                return Result.Failure<string>(DomainError.InvalidPhoneNumber);
        //        }

        //        return Result.Failure<string>(DomainError.InvalidPhoneNumber);
        //    }
        //}

        //private async Task<Result> NumberIntelligenceStatusPoolingAsync(string document, string phone, CancellationToken externalCancellationToken)
        //{
        //    using var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(26));
        //    using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(externalCancellationToken, timeoutCancellationTokenSource.Token);
        //    var cancellationToken = linkedCts.Token;

        //    while (!cancellationToken.IsCancellationRequested)
        //    {
        //        var numberIntelligence = await getNumberIntelligenceInteractor.Handle(new(ni => ni.Document == document && ni.Phone == phone), cancellationToken);

        //        if (numberIntelligence is not null)
        //        {
        //            if (numberIntelligence.Status == NumberIntelligenceStatus.Unmatched)
        //                return Result.Failure(DomainError.PhoneNotOwned);

        //            if (numberIntelligence.Status == NumberIntelligenceStatus.Matched)
        //                return Result.Success();
        //        }

        //        try
        //        {
        //            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        //        }
        //        catch (TaskCanceledException ex)
        //        {
        //            Log.Error(ex, $"Timeout ao tentar validar documento: {document} com o telefone: {phone}");

        //            break;
        //        }
        //    }

        //    if (cancellationToken.IsCancellationRequested)
        //        return Result.Failure(DomainError.NumberIntelligenceTimeout);

        //    return Result.Success();
        //}
    }
}
