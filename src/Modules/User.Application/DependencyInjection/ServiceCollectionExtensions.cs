using User.Application.UseCases.Events;
using Microsoft.Extensions.DependencyInjection;

namespace User.Application.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventInteractors(this IServiceCollection services)
            => services.AddScoped<IDeleteUserWhenUserDeletedHandler, DeleteUserWhenUserDeletedHandler>()
                       .AddScoped<IProjectUserWhenUserChangedHandler, ProjectUserWhenUserChangedHandler>()
                       .AddScoped<IProjectEmailWhenUserUpdatedHandler, ProjectEmailWhenUserUpdatedHandler>()
                       .AddScoped<IProjectPhoneWhenUserCreatedHandler, ProjectPhoneWhenUserCreatedHandler>();
    }
}
