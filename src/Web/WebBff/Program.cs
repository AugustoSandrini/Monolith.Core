using Common.DependencyInjection.Extensions;
using Core.Infrastructure.Extensions;
using CorrelationId;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using WebBff.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServicesFromAssemblies(
        builder.Configuration,
        WebBff.AssemblyReference.Assembly,
        Core.Persistence.AssemblyReference.Assembly)
    .InstallModulesFromAssemblies(
        builder.Configuration,
        User.Infrastructure.AssemblyReference.Assembly);

#if !DEBUG
builder.ConfigureSystemsManager();
#endif

builder.Services
    .AddHttpContextAccessor();

builder
    .ConfigureLogging(builder.Configuration)
    .ConfigureServiceProvider()
    .ConfigureAppConfiguration();

builder.Services
    .AddCorrelationId();

builder.Services.AddMiddlewares();

builder.Services
    .AddEndpointsApiExplorer();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddHealthCheck(builder.Configuration);


#if !DEBUG
var port = "8080";
#endif

builder.Services.AddHttpLogging(options
    => options.LoggingFields = HttpLoggingFields.All);

builder.Services.AddCommonServiceCollection(builder.Configuration);

builder.Services
    .AddControllers()
    .AddApplicationPart(WebBff.AssemblyReference.Assembly);

builder.Services.AddMiddlewares();

var app = builder.Build();

app.UsePathBase("/api");
app.UseStaticFiles();

app.MapHealthChecks("/api/healthz", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).ShortCircuit();

app.UseSwagger()
   .UseSwaggerUI()
   .UseCors(corsPolicyBuilder =>
        corsPolicyBuilder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());

app.UseCorrelationId();
app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging()
   .UseHttpsRedirection();

app.UseMiddlewares();

app.MapControllers();

try
{
    app.UseRecurringJobs();
  
    await app.RunAsync();

    Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}