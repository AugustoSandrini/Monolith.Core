using Core.Application.Services;
using User.Application.UseCases.Commands;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace User.Application.Extensions
{
    public static class HostExtensions
    {
        public static void UseUserRecurringJobs(this IHost host)
        {
            using var scoped = host.Services.CreateScope();
            var jobScheduler = scoped.ServiceProvider.GetRequiredService<IJobSchedulerService>();

            jobScheduler.ScheduleRecurring("Expire credit consultation", Cron.Daily(), new ExpireCreditConsultationCommand());
            jobScheduler.ScheduleRecurring("Expire NumberIntelligence", Cron.Daily(), new ExpireNumberIntelligenceCommand());
        }
    }
}
