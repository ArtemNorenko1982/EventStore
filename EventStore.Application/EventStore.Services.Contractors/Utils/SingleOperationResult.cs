using EventStore.DataContracts.Interfaces;
using EventStore.Services.Contractors.Utils;

namespace EventStore.Services.Contractors
{
    /// <summary>
    /// Encapsulates result of single operation.
    /// </summary>
    /// <typeparam name="TModel">Type of value returned by operation.</typeparam>
    public class SingleOperationResult<TModel> : OperationResult
        where TModel : class, IBaseModel, new()
    {
        public SingleOperationResult(OperationTypes operationType, bool wasSuccessful, string message, TModel record)
            : base(operationType, wasSuccessful, message)
        {
            Record = record;
        }

        /// <summary>
        /// Resulting value.
        /// </summary>
        public TModel Record { get; set; }
    }
}
