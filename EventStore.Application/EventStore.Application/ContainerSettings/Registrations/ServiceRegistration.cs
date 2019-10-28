using EventStore.CommonContracts;
using EventStore.Services.Contractors.Interfaces;
using EventStore.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api.ContainerSettings.Registrations
{
    /// <summary>
    /// Uses through service registration process
    /// </summary>
    public class ServiceRegistration : IRegistrationModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddTransient<IEventDataService, EventDataService>();
            services.AddTransient<IPersonDataService, PersonDataService>();
            services.AddTransient<IDataMinerService, DataMinerService>();
        }
    }
}
