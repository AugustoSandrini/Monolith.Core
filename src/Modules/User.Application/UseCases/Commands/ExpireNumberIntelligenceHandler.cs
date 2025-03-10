using Core.Application.Messaging;
using Core.Shared.Results;

namespace User.Application.UseCases.Commands
{
    public record ExpireNumberIntelligenceCommand : ICommand;

    internal class ExpireNumberIntelligenceHandler(
        //DeleteNumberIntelligenceInteractor deleteNumberIntelligenceInteractor,
        //ListNumberIntelligenceInteractor listNumberIntelligenceQuery
        ) : ICommandHandler<ExpireNumberIntelligenceCommand>
    {
        public async Task<Result> Handle(ExpireNumberIntelligenceCommand request, CancellationToken cancellationToken)
        {
            var deleteDate = DateTimeOffset.Now.AddMonths(-1);

            //var numberIntelligences = await listNumberIntelligenceQuery.Handle(new(x => x.CreatedAt <= deleteDate), cancellationToken);

            //if (numberIntelligences is null || numberIntelligences.Count == 0)
            //    return Result.Success();

            //var deletionTasks = numberIntelligences.Select(number => deleteNumberIntelligenceInteractor.Handle(new(number.Id), cancellationToken));

            //await Task.WhenAll(deletionTasks);

            return Result.Success();
        }
    }
}
