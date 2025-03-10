using User.Domain;
using User.Domain.Enumerations;
using MongoDB.Driver;
using Serilog;

namespace User.Application.UseCases.Events
{
    using Core.Application.EventBus;
    using User.Persistence.Projections;

    public interface IProjectCreditConsultationWhenCreditConsultationChangedHandler :
        IEventHandler<DomainEvent.CreditConsultationCreated>,
        IEventHandler<DomainEvent.CreditConsultationAccepted>,
        IEventHandler<DomainEvent.CreditConsultationExpired>,
        IEventHandler<DomainEvent.CreditConsultationRefused>;

    public class ProjectCreditConsultationWhenCreditConsultationChangedHandler(
        IUserProjection<Projection.CreditConsultation> creditConsultationProjection,
        ILogger logger) : IProjectCreditConsultationWhenCreditConsultationChangedHandler
    {
        public async Task Handle(DomainEvent.CreditConsultationCreated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await creditConsultationProjection.ReplaceInsertAsync(new(
                    @event.CreditConsultationId,
                    @event.OrderId,
                    @event.UserId,
                    CreditConsultationStatus.Pending,
                    string.Empty,
                    DateTimeOffset.MinValue,
                    DateTimeOffset.MinValue,
                    @event.CreatedAt),
                 cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar criação de consulta de credito para o pedido: {@event.OrderId}.");

                throw;
            }
        }

        public async Task Handle(DomainEvent.CreditConsultationAccepted @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var collection = creditConsultationProjection.GetCollection();

                var update = Builders<Projection.CreditConsultation>.Update
                    .Set(creditConsultation => creditConsultation.Status, CreditConsultationStatus.Accepted.ToString())
                    .Set(creditConsultation => creditConsultation.DecisionIp, @event.DecisionIp)
                    .Set(creditConsultation => creditConsultation.DecidedAt, @event.DecidedAt)
                    .Set(creditConsultation => creditConsultation.ExpireAt, @event.ExpireAt);

                await collection.UpdateOneAsync(
                    filter: creditConsultation => creditConsultation.Id == @event.CreditConsultationId,
                    update: update,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar aceitação de consulta de credito para o pedido: {@event.OrderId}.");

                throw;
            }
        }

        public async Task Handle(DomainEvent.CreditConsultationExpired @event, CancellationToken cancellationToken = default)
        {
            try
            {
                await creditConsultationProjection.UpdateOneFieldAsync(
                    id: @event.CreditConsultationId,
                    field: creditConsultation => creditConsultation.Status,
                    value: CreditConsultationStatus.Expired.ToString(),
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar expiração de consulta de credito para o pedido: {@event.OrderId}.");

                throw;
            }
        }

        public async Task Handle(DomainEvent.CreditConsultationRefused @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var collection = creditConsultationProjection.GetCollection();

                var update = Builders<Projection.CreditConsultation>.Update
                    .Set(creditConsultation => creditConsultation.Status, CreditConsultationStatus.Refused.ToString())
                    .Set(creditConsultation => creditConsultation.DecisionIp, @event.DecisionIp)
                    .Set(creditConsultation => creditConsultation.DecidedAt, @event.DecidedAt);

                await collection.UpdateOneAsync(
                    filter: creditConsultation => creditConsultation.Id == @event.CreditConsultationId,
                    update: update,
                    cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar recusar de consulta de credito para o pedido: {@event.OrderId}.");

                throw;
            }
        }
    }
}
