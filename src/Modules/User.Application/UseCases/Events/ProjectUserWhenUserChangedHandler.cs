using Core.Application.EventBus;
using User.Domain;
using User.Domain.Enumerations;
using User.Domain.Exceptions;
using MongoDB.Driver;
using Serilog;

namespace User.Application.UseCases.Events
{
    using User.Application.Services;
    using User.Domain.Aggregates;
    using User.Persistence.Projections;
    using MediatR;

    public interface IProjectUserWhenUserChangedHandler :
        IEventHandler<DomainEvent.UserCreated>,
        IEventHandler<DomainEvent.AddressUpserted>,
        IEventHandler<DomainEvent.UserVindiExternalIdLinked>,
        IEventHandler<DomainEvent.LastTokenSentAtUpdated>,
        IEventHandler<DomainEvent.UserActiveStatus>,
        IEventHandler<DomainEvent.UserSaleInProgressStatus>,
        IEventHandler<DomainEvent.UserDefaulterStatus>,
        IEventHandler<DomainEvent.UserPendingDebtStatus>,
        IEventHandler<DomainEvent.ProfileUpdated>;

    public class ProjectUserWhenUserChangedHandler(
        IUserProjection<Projection.User> UserProjection,
        IUserApplicationService applicationService,
        ISender sender,
        ILogger logger) : IProjectUserWhenUserChangedHandler
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
                    string.Empty,
                    DateTimeOffset.MinValue,
                    null,
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

        public async Task Handle(DomainEvent.LastTokenSentAtUpdated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await UserProjection.UpdateOneFieldAsync(
                    id: @event.UserId,
                    field: User => User.LastTokenSentAt,
                    value: @event.SentAt,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar data de envio de token de cliente: {@event.UserId}.");

                throw;
            }
        }

        public async Task Handle(DomainEvent.UserVindiExternalIdLinked @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await UserProjection.UpdateOneFieldAsync(
                    id: @event.UserId,
                    field: User => User.VindiExternalId,
                    value: @event.VindiExternalId,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar vindi ID externo de cliente: {@event.UserId}.");

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
        public async Task Handle(DomainEvent.UserPendingDebtStatus @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await UpdateStatusAsync(@event.UserId, @event.Status, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar status de cliente como divida pendente: {@event.UserId}.");

                throw;
            }
        }

        public async Task Handle(DomainEvent.UserSaleInProgressStatus @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await UpdateStatusAsync(@event.UserId, @event.Status, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar status cliente como compra em progresso: {@event.UserId}.");

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

        public async Task Handle(DomainEvent.ProfileUpdated @event, CancellationToken cancellationToken = default)
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
