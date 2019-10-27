using EventStore.DataContracts.Interfaces;

namespace EventStore.Services.Contractors.Interfaces.Core
{
    /// <summary>
    /// Contractor represents read data service
    /// </summary>
    public interface IReadDataService<TModel> : IDataService
        where TModel : class, IBaseModel, new()
    {
        /// <summary>
        /// Get one record by id
        /// </summary>
        /// <param name="id">Record id</param>
        /// <returns>model object</returns>
        SingleOperationResult<TModel> GetSingleRecord(int id);

        /// <summary>
        /// Get a record by parent ID
        /// </summary>
        /// <param name="sourceId">int</param>
        /// <param name="id"></param>
        /// <returns>model</returns>
        SingleOperationResult<TModel> GetSingleRecordBySource(int sourceId, int id);
    }
}
