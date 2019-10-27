using System.Threading;
using System.Threading.Tasks;
using EventStore.Services.Contractors.Interfaces;
using Microsoft.Extensions.Hosting;
using SystemTimer = System.Timers.Timer;

namespace EventStore.Api.ContainerSettinigs.HostedServices
{
    using System.Timers;
    public class KafkaListener : IHostedService
    {
        private readonly SystemTimer timer;
        private readonly IDataMinerService minerService;
        public KafkaListener(IDataMinerService minerService)
        {
            timer = new SystemTimer(20000);
            this.minerService = minerService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer.Elapsed += async (obj, e) => await OnTimerElapsed(obj, e);
            timer.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Stop();
            return Task.CompletedTask;
        }

        private async Task OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                minerService.ConsumeMessage();
            }
            catch (TaskCanceledException)
            {
                
            }

        }
    }
}
