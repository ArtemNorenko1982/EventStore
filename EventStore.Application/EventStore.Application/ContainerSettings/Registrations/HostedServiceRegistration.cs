using EventStore.Api.ContainerSettings.HostedServices;
using EventStore.CommonContracts;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api.ContainerSettings.Registrations
{
    public class HostedServicesRegistration : IRegistrationModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddHostedService<KafkaListener>();
        }
    }
}
