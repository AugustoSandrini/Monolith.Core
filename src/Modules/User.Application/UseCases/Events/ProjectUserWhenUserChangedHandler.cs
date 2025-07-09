using Core.Application.EventBus;
using User.Domain;
using MongoDB.Driver;
using Serilog;

namespace User.Application.UseCases.Events
{
    using User.Domain.Aggregates;
    using User.Persistence.Projections;

    public interface IProjectUserWhenUserChangedHandler :
        IEventHandler<DomainEvent.UserCreated>,
        IEventHandler<DomainEvent.AddressUpserted>,
        IEventHandler<DomainEvent.UserActiveStatus>,
        IEventHandler<DomainEvent.UserDefaulterStatus>,
        IEventHandler<DomainEvent.UserUpdated>;

    public class ProjectUserWhenUserChangedHandler(IUserProjection<Projection.User> UserProjection, ILogger logger) : IProjectUserWhenUserChangedHandler
    {
        public async Task Handle(DomainEvent.UserCreated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await UserProjection.ReplaceInsertAsync(new(
                    @event.UserId,
                    string.Empty,
                    @event.Document,
                    @event.Status,
                    null,
                    DateTimeOffset.MinValue,
                    @event.CreatedAt),
                 cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar criação de cliente: {@event.UserId}.");

                throw;
            }
        }

        public async Task Handle(DomainEvent.AddressUpserted @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await UserProjection.UpdateOneFieldAsync(
                    id: @event.UserId,
                    field: User => User.Address,
                    value: @event.Address,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar endereço de cliente: {@event.UserId}.");

                throw;
            }
        }

        public async Task Handle(DomainEvent.UserActiveStatus @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await UpdateStatusAsync(@event.UserId, @event.Status, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar status de cliente como ativo: {@event.UserId}.");

                throw;
            }
        }

        public async Task Handle(DomainEvent.UserDefaulterStatus @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await UpdateStatusAsync(@event.UserId, @event.Status, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar status cliente como padrão: {@event.UserId}.");

                throw;
            }
        }

        public async Task Handle(DomainEvent.UserUpdated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var collection = UserProjection.GetCollection();

                var update = Builders<Projection.User>.Update
                    .Set(User => User.Name, @event.Name)
                    .Set(User => User.Status, @event.Status)
                    .Set(User => User.DateOfBirth, @event.DateOfBirth);

                await collection.UpdateOneAsync(
                    filter: User => User.Id == @event.UserId,
                    update: update,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar atualização de cliente: {@event.UserId}.");

                throw;
            }
        }

        private async Task UpdateStatusAsync(Guid UserId, string status, CancellationToken cancellationToken = default)
            => await UserProjection.UpdateOneFieldAsync(
                id: UserId,
                field: User => User.Status,
                value: status,
                cancellationToken: cancellationToken);
    }
}
