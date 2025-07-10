using Core.Application.EventBus;
using User.Domain;
using MongoDB.Driver;
using Serilog;

namespace User.Application.UseCases.Events
{
    using User.Application.Extensions;
    using User.Domain.Aggregates;
    using User.Persistence.Projections;

    public interface IProjectUserWhenUserChangedHandler :
        IEventHandler<DomainEvent.UserCreated>,
        IEventHandler<DomainEvent.AddressUpserted>,
        IEventHandler<DomainEvent.UserActiveStatus>,
        IEventHandler<DomainEvent.UserDefaulterStatus>,
        IEventHandler<DomainEvent.UserUpdated>;

    public class ProjectUserWhenUserChangedHandler(IUserProjection<Projection.User> userProjection, ILogger logger) : IProjectUserWhenUserChangedHandler
    {
        public async Task Handle(DomainEvent.UserCreated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await userProjection.ReplaceInsertAsync(new Projection.User(
                    @event.UserId,
                    string.Empty,
                    @event.Document.RemoveNonAlphaNumericCharacters(),
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
                await userProjection.UpdateOneFieldAsync(
                    id: @event.UserId,
                    field: user => user.Address,
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
                var collection = userProjection.GetCollection();

                var update = Builders<Projection.User>.Update
                    .Set(user => user.Name, @event.Name)
                    .Set(user => user.Status, @event.Status)
                    .Set(user => user.DateOfBirth, @event.DateOfBirth);

                await collection.UpdateOneAsync(
                    filter: user => user.Id == @event.UserId,
                    update: update,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar atualização de cliente: {@event.UserId}.");

                throw;
            }
        }

        private async Task UpdateStatusAsync(Guid userId, string status, CancellationToken cancellationToken = default)
            => await userProjection.UpdateOneFieldAsync(
                id: userId,
                field: user => user.Status,
                value: status,
                cancellationToken: cancellationToken);
    }
}
