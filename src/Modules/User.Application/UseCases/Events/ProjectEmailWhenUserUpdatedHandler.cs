using User.Domain;
using Serilog;
using Core.Application.EventBus;
using User.Persistence.Projections;

namespace User.Application.UseCases.Events
{
    public interface IProjectEmailWhenUserUpdatedHandler : IEventHandler<DomainEvent.UserUpdated>;

    public class ProjectEmailWhenUserUpdatedHandler(
        IUserProjection<Projection.Email> emailProjection,
        ILogger logger) : IProjectEmailWhenUserUpdatedHandler
    {

        public async Task Handle(DomainEvent.UserUpdated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await emailProjection.ReplaceInsertAsync(new(
                    Guid.NewGuid(),
                    @event.UserId,
                    @event.Email,
                    false,
                    @event.Timestamp),
                 cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao atualizar email para o cliente: {@event.UserId}.");

                throw;
            }
        }
    }
}
