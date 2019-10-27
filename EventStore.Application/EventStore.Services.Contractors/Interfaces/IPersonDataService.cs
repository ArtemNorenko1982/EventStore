using EventStore.CommonContracts.SourceParameters;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces.Core;
using EventStore.Services.Contractors.Utils;

namespace EventStore.Services.Contractors.Interfaces
{
    public interface IPersonDataService : IWriteDataService<PersonModel>, IReadDataService<PersonModel>
    {
        /// <summary>
        /// Get all records
        /// </summary>
        /// <returns><see cref="CollectionOperationResult{TModel}"/></returns>
        CollectionOperationResult<PersonModel> GetRecords(PersonSourceParameters parameters);
    }
}
