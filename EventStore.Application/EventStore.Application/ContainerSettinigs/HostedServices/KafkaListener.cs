using System;
using System.Threading;
using System.Threading.Tasks;
using EventStore.Data.Entities;
using EventStore.DataContracts;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces;
using EventStore.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemTimer = System.Timers.Timer;

namespace EventStore.Api.ContainerSettinigs.HostedServices
{
    using System.Timers;
    public class KafkaListener : IHostedService
    {
        private readonly SystemTimer timer;
        //private readonly IDataMinerService minerService;
        private readonly IServiceProvider services;
        public KafkaListener(IServiceProvider services)
        {
            this.services = services;
            timer = new SystemTimer(200);
            //StartAsync(CancellationToken.None);
        }
        public Task StartAsync(CancellationToken token)
        {
            timer.Elapsed += async (obj, e) => await OnTimerElapsed(obj, e);
            timer.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken token)
        {
            timer.Stop();
            return Task.CompletedTask;
        }

        private Task OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //var minerService = new DataMinerService(services.GetRequiredService<IEventStoreRepository<EventModel, EventEntity>>());
            using (var scope = services.CreateScope())
            {
                var minerService = new DataMinerService(scope.ServiceProvider.GetRequiredService<IEventStoreRepository<EventModel, EventEntity>>());

                try
                {
                    minerService.ConsumeMessage();
                }
                catch (TaskCanceledException)
                {
                    var r = false;
                }
            }
            return Task.CompletedTask;
        }
    }
}
