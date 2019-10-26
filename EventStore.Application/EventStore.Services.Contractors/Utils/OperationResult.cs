namespace EventStore.Services.Contractors.Utils
{
    /// <summary>
    /// Incapsulates result of any operation.
    /// </summary>
    public class OperationResult
    {
        /// <summary>
        /// Creates object that incapsulates result of any operation
        /// <param name="operationType">Type of the operation.</param>
        /// <param name="wasSuccessful">True if operation was successful.</param>
        /// <param name="message">Error or success message for user.</param>
        /// </summary>
        public OperationResult(OperationTypes operationType, bool wasSuccessful, string message)
        {
            WasSuccessful = wasSuccessful;
            Message = message;
            OperationName = operationType.ToString();
        }

        /// <summary>
        /// Name of operation
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// True if operation was successful
        /// </summary>
        public bool WasSuccessful { get; set; }

        /// <summary>
        /// Error or success message for user
        /// </summary>
        public string Message { get; set; }
    }
}
