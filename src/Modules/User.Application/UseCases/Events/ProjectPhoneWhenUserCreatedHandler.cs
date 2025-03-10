using User.Domain;
using Serilog;
using Core.Application.EventBus;
using User.Persistence.Projections;

namespace User.Application.UseCases.Events
{
    public interface IProjectPhoneWhenUserCreatedHandler : IEventHandler<DomainEvent.UserCreated>;

    public class ProjectPhoneWhenUserCreatedHandler(
        IUserProjection<Projection.Phone> phoneProjection,
        ILogger logger) : IProjectPhoneWhenUserCreatedHandler
    {

        public async Task Handle(DomainEvent.UserCreated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await phoneProjection.ReplaceInsertAsync(new(
                    Guid.NewGuid(),
                    @event.UserId,
                    @event.Phone,
                    false,
                    @event.Timestamp),
                 cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao atualizar telefone para o cliente: {@event.UserId}.");

                throw;
            }
        }
    }
}
