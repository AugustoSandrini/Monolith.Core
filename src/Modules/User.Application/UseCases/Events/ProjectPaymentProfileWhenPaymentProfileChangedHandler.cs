using User.Domain;
using Serilog;
using Core.Application.EventBus;
using User.Persistence.Projections;
using MongoDB.Driver;

namespace User.Application.UseCases.Events
{
    public interface IProjectPaymentProfileWhenPaymentProfileChangedHandler :
        IEventHandler<DomainEvent.PaymentProfileCreated>;

    public class ProjectPaymentProfileWhenPaymentProfileChangedHandler(
        IUserProjection<Projection.PaymentProfile> paymentProfileProjection,
        ILogger logger) : IProjectPaymentProfileWhenPaymentProfileChangedHandler
    {

        public async Task Handle(DomainEvent.PaymentProfileCreated @event, CancellationToken cancellationToken = default)
        {
            try
            {
                var collection = paymentProfileProjection.GetCollection();

                var update = Builders<Projection.PaymentProfile>.Update
                    .Set(order => order.IsMain, false);

                await collection.UpdateManyAsync(
                    filter: paymentProfile => paymentProfile.UserId == @event.UserId,
                    update: update,
                    cancellationToken: cancellationToken);

                await paymentProfileProjection.ReplaceInsertAsync(new(
                    @event.PaymentProfileId,
                    @event.ExternalId.ToString(),
                    @event.UserId,
                    string.Empty,
                    string.Empty,
                    false,
                    string.Empty,
                    string.Empty,
                    @event.GatewayToken,
                    @event.CreatedAt,
                    @event.IsMainMethod),
                 cancellationToken);
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Falha ao projetar criação de perfil de pagamento para o cliente: {@event.UserId}.");

                throw;
            }
        }
    }
}
