using System;
using EventStore.Api.ContainerSettinigs.HostedServices;
using EventStore.CommonContracts;
using EventStore.Services.Contractors.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api.ContainerSettinigs.Registrations
{
    public class HostedServicesRegistration : IRegistrationModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddHostedService<KafkaListener>();
        }
    }
}
