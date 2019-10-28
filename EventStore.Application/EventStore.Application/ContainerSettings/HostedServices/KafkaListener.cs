using System;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Data.Entities;
using EventStore.DataContracts;
using EventStore.DataContracts.DTO;
using EventStore.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemTimer = System.Timers.Timer;

namespace EventStore.Api.ContainerSettings.HostedServices
{
    using System.Timers;
    public class KafkaListener : IHostedService
    {
        private readonly SystemTimer _timer;
        private readonly IServiceProvider _services;
        public KafkaListener(IServiceProvider services)
        {
            _services = services;
            _timer = new SystemTimer(200);
        }

        public Task StartAsync(CancellationToken token)
        {
            _timer.Elapsed += async (obj, e) => await OnTimerElapsed(obj, e);
            _timer.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken token)
        {
            _timer.Stop();
            _timer.Elapsed -= async (obj, e) => await OnTimerElapsed(obj, e);
            return Task.CompletedTask;
        }

        private Task OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            using (var scope = _services.CreateScope())
            {
                var minerService = new DataMinerService(scope.ServiceProvider.GetRequiredService<IEventStoreRepository<EventModel, EventEntity>>());

                try
                {
                    minerService.ConsumeMessage();
                }
                catch (TaskCanceledException)
                {
                    //TODO: handle exception
                }
            }
            return Task.CompletedTask;
        }
    }
}
