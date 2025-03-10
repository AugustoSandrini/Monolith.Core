using Core.Application.Messaging;
using Core.Shared.Errors;
using Core.Shared.Results;
using User.Application.Errors.Validation;
using User.Shared.Commands;

namespace User.Application.UseCases.Commands
{
    public class UpdateUserNumberIntelligenceHandler(
        //UpdateNumberIntelligenceStatusInteractor updateNumberIntelligenceInteractor,
        //GetNumberIntelligenceInteractor getNumberIntelligenceInteractor
        ) : ICommandHandler<UpdateUserNumberIntelligenceCommand>
    {
        public async Task<Result> Handle(UpdateUserNumberIntelligenceCommand cmd, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);

            //var numberIntelligence = await getNumberIntelligenceInteractor.Handle(new(ni => ni.Token == cmd.Token), cancellationToken);

            //if (numberIntelligence is null)
            //    return Result.Failure(new NotFoundError(DomainError.NumberIntelligenceNotFound));

            //await updateNumberIntelligenceInteractor.Handle(new(cmd.Token, cmd.Matched.Equals("MATCHED", StringComparison.OrdinalIgnoreCase) ? true : false), cancellationToken);

            return Result.Success();
        }
    }
}
