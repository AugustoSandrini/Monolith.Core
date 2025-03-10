using User.Application.UseCases.Events;
using Microsoft.Extensions.DependencyInjection;

namespace User.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventInteractors(this IServiceCollection services)
            => services
                      .AddScoped<ICreateUserWhenUserDocumentAndPhoneValidatedHandler, CreateUserWhenUserDocumentAndPhoneValidatedHandler>()
                      .AddScoped<ICreateVindiUserWhenProfileUpdatedHandler, CreateVindiUserWhenProfileUpdatedHandler>()
                      .AddScoped<IDeleteUserWhenUserDeletedHandler, DeleteUserWhenUserDeletedHandler>()
                      .AddScoped<IProjectCreditConsultationWhenCreditConsultationChangedHandler, ProjectCreditConsultationWhenCreditConsultationChangedHandler>()
                      .AddScoped<IProjectUserWhenUserChangedHandler, ProjectUserWhenUserChangedHandler>()
                      .AddScoped<IProjectEmailWhenProfileUpdatedHandler, ProjectEmailWhenProfileUpdatedHandler>()
                      .AddScoped<IProjectPaymentProfileWhenPaymentProfileChangedHandler, ProjectPaymentProfileWhenPaymentProfileChangedHandler>()
                      .AddScoped<IProjectPhoneWhenUserCreatedHandler, ProjectPhoneWhenUserCreatedHandler>()
                      .AddScoped<ISendSmsTokenWhenCreditConsultationCreatedHandler, SendSmsTokenWhenCreditConsultationCreatedHandler>()
                      .AddScoped<ISendToBmpWhenAddressUpsertHandler, SendToBmpWhenAddressUpsertHandler>()
                      .AddScoped<ISendToBmpWhenProfileUpdatedHandler, SendToBmpWhenProfileUpdatedHandler>()
                      .AddScoped<IUpdateLastTokenSentAtWhenTokenSentHandler, UpdateLastTokenSentAtWhenTokenSentHandler>()
                      .AddScoped<ISendEmailWhenCreditConsultationAcceptedHandler, SendEmailWhenCreditConsultationAcceptedHandler>()
                      .AddScoped<ISendEmailWhenCreditConsultationCreatedHandler, SendEmailWhenCreditConsultationCreatedHandler>();
    }
}
