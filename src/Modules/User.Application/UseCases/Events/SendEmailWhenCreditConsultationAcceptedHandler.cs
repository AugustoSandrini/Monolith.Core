using Common.Options;
using Core.Application.EventBus;
using Core.Shared.Results;
using User.Application.Emails;
using User.Domain;
using User.Persistence.Projections;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;

namespace User.Application.UseCases.Events
{
    public interface ISendEmailWhenCreditConsultationAcceptedHandler : IEventHandler<DomainEvent.CreditConsultationAccepted>;
    public class SendEmailWhenCreditConsultationAcceptedHandler(
        ISender sender,
        IOptions<EnvironmentOptions> environment,
        //ICommunicationService communicationService,
        IUserProjection<Projection.User> UserProjectionGateway,
        IUserProjection<Projection.CreditConsultation> creditConsultationProjectionGateway) : ISendEmailWhenCreditConsultationAcceptedHandler
    {
        public async Task Handle(DomainEvent.CreditConsultationAccepted @event, CancellationToken cancellationToken = default)
        {
            //var orderResult = await sender.Send(new GetOrderByIdQuery(@event.OrderId), cancellationToken);

            //if (orderResult.IsFailure)
            //    throw new OrderNotFoundException(@event.OrderId);

            //var order = orderResult.Value;

            var creditConsultation = await creditConsultationProjectionGateway.FindAsync(x => x.Id == @event.CreditConsultationId, cancellationToken);

            //var User = await UserProjectionGateway.FindAsync(x => x.Id == order.UserId, cancellationToken);
            
            //var subject = "Nova solicitação de análise de crédito!";

            //var htmlParameters = new Dictionary<string, string>
            //{
            //    {
            //        "{{REQUEST_DATE}}", $"{creditConsultation.CreatedAt:dd/MM/yyyy HH:mm}"
            //    },
            //    {
            //        "{{DOCUMENT}}", $"{Extensions.StringExtensions.FormatCpf(User.Document)}"
            //    },
            //    {
            //        "{{REQUESTED_AMOUNT}}", $"{order.OriginalRequestedAmount.ToString("C", new CultureInfo("pt-BR"))}"
            //    },
            //    {
            //        "{{REFERENCE}}", $"{order.Number}"
            //    }
            //};

            //await SendEmailAsync(GetEmailAddressesByEnvironment(), subject, new Templates.CreditConsultation().GetHtmlString(), htmlParameters, cancellationToken);
        }

        //private async Task SendEmailAsync(List<string> emails, string subject, string html, Dictionary<string, string> htmlParameters = null, CancellationToken cancellationToken = default)
        //{
        //    var request = new SendEmailRequest(emails, subject, html, htmlParameters);

        //    await communicationService.SendEmailAsync(request, cancellationToken);
        //}

        private List<string> GetEmailAddressesByEnvironment()
            => environment.Value.EnvironmentName switch
            {
                "PRODUCTION" => ["emaisl"],
                "STAGING" => ["emails"],
                _ => [""],
            };
    }
}
