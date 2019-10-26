using Microsoft.Extensions.DependencyInjection;

namespace EventStore.CommonContracts
{
    public interface IRegistrationModule
    {
        void Register(IServiceCollection services);
    }
}
