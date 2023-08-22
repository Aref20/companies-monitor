using Quartz.Spi;
using Quartz;
using Microsoft.Extensions.Hosting;

namespace MSGCompaniesMonitor.Jobs
{
    public class QuartzHostedService : IHostedService
    {

        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;

        public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            scheduler.JobFactory = _jobFactory;

            var jobDetail = JobBuilder.Create<SendEmailJob>()
                .WithIdentity("sendEmailJob", "group1")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("sendEmailTrigger", "group1")
                .StartNow()
                .WithCronSchedule("0/1 * * * * ?")//("0 0 8,18 * * ?") // Execute at 8 AM and 6 PM
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
            await scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            await scheduler.Shutdown(cancellationToken);
        }
    }
}
