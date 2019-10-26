using EventStore.CommonContracts;
using EventStore.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api.ContainerSettinigs.Registrations
{
    public class DbRegistration : IRegistrationModule
    {
        public void Register(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("EventStoreDB");

            services.AddDbContext<EventStoreContext>(x => x.UseNpgsql(connectionString));
        }
    }
}
