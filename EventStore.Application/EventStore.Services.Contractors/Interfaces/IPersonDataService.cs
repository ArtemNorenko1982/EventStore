using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces.Core;

namespace EventStore.Services.Contractors.Interfaces
{
    public interface IPersonDataService : IWriteDataService<PersonModel>, IReadDataService<PersonModel>
    {
    }
}
