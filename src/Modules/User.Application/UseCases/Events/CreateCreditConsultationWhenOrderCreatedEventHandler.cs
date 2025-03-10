using User.Domain.Exceptions;
//using OrderDomainEvent = Order.Domain.DomainEvent;
using Serilog;

namespace User.Application.UseCases.Events
{
    using Core.Application.EventBus;
    using User.Application.Services;
    using User.Domain.Aggregates;

    //public interface ICreateCreditConsultationWhenOrderCreatedEventHandler : IEventHandler<OrderDomainEvent.OrderCreated>;

    //public class CreateCreditConsultationWhenOrderCreatedEventHandler(
    //    IUserApplicationService applicationService,
    //    ILogger logger) : ICreateCreditConsultationWhenOrderCreatedEventHandler
    //{

    //    public async Task Handle(OrderDomainEvent.OrderCreated @event, CancellationToken cancellationToken = default)
    //    {
    //        try
    //        {
    //            var UserResult = await applicationService.LoadAggregateAsync<User>(@event.UserId, cancellationToken);

    //            if (UserResult.IsFailure)
    //                throw new UserNotFoundException(@event.UserId);

    //            var User = UserResult.Value;

    //            User.CreateCreditConsultation(@event.OrderId);

    //            await applicationService.AppendEventsAsync(User, cancellationToken);
    //        }
    //        catch (Exception ex)
    //        {
    //            logger.Error(ex, $"Falha ao criar consulta de credito para o pedido: {@event.OrderId}.");

    //            throw;
    //        }
    //    }
    //}
}
