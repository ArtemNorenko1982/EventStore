using EventStore.CommonContracts.Helpers;
using EventStore.DataContracts.Interfaces;

namespace EventStore.Services.Contractors.Utils
{
    /// <summary>
    /// Encapsulates result of operation for list of objects.
    /// </summary>
    /// <typeparam name="TModel">Type of value returned by operation.</typeparam>
    public class CollectionOperationResult<TModel> : OperationResult where TModel : class, IBaseModel
    {
        public CollectionOperationResult(OperationTypes operationType, bool wasSuccessful, string message, PagesList<TModel> models)
            : base(operationType, wasSuccessful, message)
        {
            Records = models;
        }

        public CollectionOperationResult(OperationTypes operationType, bool wasSuccessful, string message)
            : base(operationType, wasSuccessful, message)
        {
        }

        /// <summary>
        /// Resulting value.
        /// </summary>
        public PagesList<TModel> Records { get; set; }
    }
}
