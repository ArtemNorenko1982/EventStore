using EventStore.CommonContracts.SourceParameters;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces.Core;
using EventStore.Services.Contractors.Utils;

namespace EventStore.Services.Contractors.Interfaces
{
    public interface IEventDataService : IWriteDataService<EventModel>, IReadDataService<EventModel>
    {
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns><see cref="CollectionOperationResult{TModel}"/></returns>
        CollectionOperationResult<EventModel> GetRecords(EventSourceParameters parameters);
    }
}
