using EventStore.CommonContracts;
using EventStore.Data;
using EventStore.Data.Entities;
using EventStore.Data.Interfaces;
using EventStore.DataContracts.DTO;
using EventStore.DataContracts.Interfaces;
using EventStore.Services.Contractors.Interfaces;
using EventStore.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api.ContainerSettinigs.Registrations
{
    /// <summary>
    /// Uses through service registration process
    /// </summary>
    public class ServiceRegistration : IRegistrationModule
    {
        public void Register(IServiceCollection services)
        {
            //services.AddTransient<IBaseModel, EventModel>();
            //services.AddTransient<IBaseModel, PersonModel>();
            //services.AddTransient<IBaseEntity, EventEntity>();
            //services.AddTransient<IBaseEntity, PersonEntity>();

            services.AddTransient<IDataService, DataService<IBaseModel, IBaseEntity>>();
            services.AddTransient<IReadDataService<DataService, DataService<PersonModel, PersonEntity>>();
            services.AddTransient<IEventDataService, EventDataService>();
            services.AddTransient<IPersonDataService, PersonDataService>();
            services.AddTransient<IDataMinerService, DataMinerService>();
        }
    }
}
