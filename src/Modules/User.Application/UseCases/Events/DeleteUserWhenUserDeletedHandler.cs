using User.Domain;
using Serilog;

namespace User.Application.UseCases.Events
{
    using Core.Application.EventBus;
    using User.Persistence.Projections;

    public interface IDeleteUserWhenUserDeletedHandler : IEventHandler<DomainEvent.UserDeleted>;

    public class DeleteUserWhenUserDeletedHandler(
        IUserProjection<Projection.User> UserProjection,
        ILogger logger) : IDeleteUserWhenUserDeletedHandler
    {
        public async Task Handle(DomainEvent.UserDeleted @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await UserProjection.DeleteAsync(x => x.Id == @event.UserId, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao deletar o usuário: {@event.UserId}.");

                throw;
            }
        }
    }
}
