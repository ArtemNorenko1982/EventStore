using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces.Core;

namespace EventStore.Services.Contractors.Interfaces
{
    public interface IEventDataService : IWriteDataService<EventModel>, IReadDataService<EventModel>
    {
    }
}
