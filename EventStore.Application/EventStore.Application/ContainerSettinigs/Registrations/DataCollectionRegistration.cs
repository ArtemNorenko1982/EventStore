using EventStore.CommonContracts;
using EventStore.Data;
using EventStore.Data.Entities;
using EventStore.DataAccess.DataAccess;
using EventStore.DataContracts;
using EventStore.DataContracts.DTO;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api.ContainerSettinigs.Registrations
{
    public class DataCollectionRegistration : IRegistrationModule
    {
        public void Register(IServiceCollection services)
        {
            services.AddScoped<IEventStoreRepository<PersonModel, PersonEntity>, EventStoreRepository<PersonModel, PersonEntity>>();
            services.AddScoped<IEventStoreRepository<EventModel, EventEntity>, EventStoreRepository<EventModel, EventEntity>>();
        }
    }
}
