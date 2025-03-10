using Core.Application.EventBus;
using User.Application.Emails;
using User.Domain;
using User.Domain.Enumerations;
using User.Domain.Exceptions;
using User.Persistence.Projections;
using MediatR;
using System.Globalization;

namespace User.Application.UseCases.Events
{
    public interface ISendEmailWhenCreditConsultationCreatedHandler : IEventHandler<DomainEvent.CreditConsultationCreated>;
    public class SendEmailWhenCreditConsultationCreatedHandler(
        ISender sender,
        //ICommunicationService communicationService,
        IUserProjection<Projection.User> UserProjectionGateway,
        IUserProjection<Projection.Email> UserEmailProjectionGateway) : ISendEmailWhenCreditConsultationCreatedHandler
    {
        public async Task Handle(DomainEvent.CreditConsultationCreated @event, CancellationToken cancellationToken = default)
        {
            //var User = await UserProjectionGateway.FindAsync(x => x.Id == @event.UserId && x.Status != UserStatus.PendingProfile, cancellationToken);

            //if (User is null)
            //    return;

            //var details = await GetEmailDetailsAsync(User, @event.OrderId, cancellationToken);

            //var subject = "Autorização para análise de crédito";

            //var htmlParameters = new Dictionary<string, string>
            //{              
            //    {
            //        "{{User_NAME}}", $"{User.Name}"
            //    },
            //    {
            //        "{{ESTABLISHMENT_NAME}}", $"{details.Establishment.TradeName}"
            //    },
            //    {
            //        "{{REQUESTED_AMOUNT}}", $"{details.Order.OriginalRequestedAmount.ToString("C", new CultureInfo("pt-BR"))}"
            //    }
            //};
            
            //await SendEmailAsync([details.UserEmail.Address], subject, new Templates.CreditAnalysis().GetHtmlString(), htmlParameters, cancellationToken);
        }

        //private async Task<EmailDetails> GetEmailDetailsAsync(Projection.User User, Guid orderId, CancellationToken cancellationToken)
        //{
        //    var UserEmail = await UserEmailProjectionGateway.FindAsync(x => x.UserId == User.Id, cancellationToken);

        //    var orderResult = await sender.Send(new GetOrderByIdQuery(orderId), cancellationToken);
            
        //    if (orderResult.IsFailure)
        //        throw new OrderNotFoundException(orderId);

        //    var order = orderResult.Value;

        //    var establishmentResult = await sender.Send(new GetEstablishmentByIdQuery(order.EstablishmentId), cancellationToken);

        //    if (establishmentResult.IsFailure)
        //        throw new EstablishmentNotFoundException(order.EstablishmentId);

        //    var establishment = establishmentResult.Value;

        //    return new(order, establishment, UserEmail);
        //}

        //public record EmailDetails(OrderResponse Order, EstablishmentResponse Establishment, Projection.Email UserEmail);
        
        //private async Task SendEmailAsync(List<string> emails, string subject, string html, Dictionary<string, string> htmlParameters = null, CancellationToken cancellationToken = default)
        //{
        //    var request = new SendEmailRequest(emails, subject, html, htmlParameters);

        //    await communicationService.SendEmailAsync(request, cancellationToken);
        //}
    }
}
