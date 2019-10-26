using System.Collections.Generic;
using EventStore.DataContracts.Interfaces;
using EventStore.Services.Contractors.Utils;

namespace EventStore.Services.Contractors.Interfaces.Core
{
    /// <summary>
    /// Contractor represents write data service
    /// </summary>
    public interface IWriteDataService<TModel> : IDataService
        where TModel : class, IBaseModel, new()
    {
        /// <summary>
        /// Method for adding new entity into database
        /// </summary>
        /// <param name="newItem"></param>
        SingleOperationResult<TModel> Add(TModel newItem);

        /// <summary>
        /// Method for adding range of new entities into database.
        /// </summary>
        CollectionOperationResult<TModel> AddRange(IEnumerable<TModel> items);

        /// <summary>
        /// Method for updating entity in data base
        /// </summary>
        /// <param name="item"></param>
        SingleOperationResult<TModel> Update(TModel item);

        /// <summary>
        /// Method for updating range of entities in data base
        /// </summary>
        /// <param name="items"></param>
        CollectionOperationResult<TModel> UpdateRange(IEnumerable<TModel> items);

        /// <summary>
        /// Method for removing record from database by entity id
        /// </summary>
        /// <param name="id">id of model to be deleted</param>
        SingleOperationResult<TModel> RemoveById(int id);

        /// <summary>
        /// Base method for update, delete and add operations for one model (type of operation is passed by 'operation' parameter)
        /// </summary>
        /// <param name="model">Full model to perform operation</param>
        /// <param name="operation">Operation type</param>
        /// <returns>Model response</returns>
        SingleOperationResult<TModel> SaveChanges(TModel model, OperationTypes operation);

        /// <summary>
        /// Base method for update, delete and add operations for collection of models (type of operation is passed by 'operation' parameter) 
        /// </summary>
        /// <param name="models">Models to save.</param>
        /// <param name="operation">Type of operation to be performed.</param>
        IEnumerable<SingleOperationResult<TModel>> SaveChanges(IEnumerable<TModel> models, OperationTypes operation);
    }
}
