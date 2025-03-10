using Core.Application.Behaviors;
using Core.Infrastructure.Configuration;
using Core.Shared.Extensions;
using User.Application.Services;
using User.Infrastructure.Services;
using User.Application.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace User.Infrastructure.ServiceInstallers
{
    /// <summary>
    /// Represents the Users module application service installer.
    /// </summary>
    internal sealed class ApplicationServiceInstaller : IServiceInstaller
    {
        /// <inheritdoc />
        public void Install(IServiceCollection services, IConfiguration configuration) =>
            services
                .Tap(services.TryAddTransient<IUserApplicationService, UserApplicationService>)
                .AddEventInteractors()
                .AddMediatR(config =>
                {
                    config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);

                    config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                    config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                })
                .AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);
    }
}
