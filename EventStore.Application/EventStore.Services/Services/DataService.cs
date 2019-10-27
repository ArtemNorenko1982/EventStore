using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using EventStore.CommonContracts.Helpers;
using EventStore.Data.Interfaces;
using EventStore.DataContracts;
using EventStore.DataContracts.Interfaces;
using EventStore.Services.Contractors;
using EventStore.Services.Contractors.Interfaces;
using EventStore.Services.Contractors.Utils;
using Microsoft.Extensions.Logging;

namespace EventStore.Services.Services
{
    public class DataService<TModel, TEntity> : IDataService
         where TModel : class, IBaseModel, new()
         where TEntity : class, IBaseEntity
    {
        /// <summary>
        /// Object used to log events.
        /// </summary>
        protected ILogger Logger;

        /// <summary>
        /// Repository to work with database
        /// </summary>
        protected IEventStoreRepository<TModel, TEntity> Repository;

        protected DataService(IEventStoreRepository<TModel, TEntity> repository)
        {
            Repository = repository;
        }

        #region Helpers Methods
        /// <summary>
        /// Log exception using default logger
        /// </summary>
        /// <param name="ex">Exception object</param>
        /// <param name="memberName">Caller member name</param>
        protected void LogException(Exception ex, [CallerMemberName]string memberName = "")
        {
            Logger.LogError("{0} - {1} \n {2}", memberName, ex.Message, ex.StackTrace);
        }

        /// <summary>
        /// Get operation result with error and custom message for single record
        /// </summary>
        /// <param name="operation">Operation Type</param>
        /// <param name="customMessage">Custom error message</param>
        /// <param name="model">Model that has been used for operation</param>
        /// <returns>Operation result (Single/Error)</returns>
        protected SingleOperationResult<TModel> SingleError(OperationTypes operation, string customMessage, TModel model = default(TModel))
        {
            return new SingleOperationResult<TModel>(operation, false, customMessage, model);
        }

        protected SingleOperationResult<TModel> SingleError(OperationTypes operation, TModel model = null)
        {
            return new SingleOperationResult<TModel>(operation, false, ServerMessages.CannotPerformOperation, model);
        }

        /// <summary>
		/// Get operation result with validation errors for single record
		/// </summary>
		/// <param name="operation">Operation Type</param>
		/// <param name="model">Model that has been used for operation</param>
		/// <param name="validatinResults">Results of validation</param>
		/// <returns>Operation result (Single/Error)</returns>
        protected SingleOperationResult<TModel> SingleError(OperationTypes operation, TModel model, IEnumerable<ValidationResult> validationResult)
        {
            return new SingleOperationResult<TModel>(operation, false, validationResult.ToString(), model);
        }

        protected SingleOperationResult<TModel> SingleError(OperationTypes operation, TModel model, Func<OperationTypes, TModel, bool, string> retrieveMessageFunc)
        {
            var message = retrieveMessageFunc(operation, model, false);
            return new SingleOperationResult<TModel>(operation, false, message, model);
        }

        //single success
        protected SingleOperationResult<TModel> SingleSuccess(OperationTypes operation, string customMessage, TModel model = default(TModel))
        {
            return new SingleOperationResult<TModel>(operation, true, customMessage, model);
        }

        protected SingleOperationResult<TModel> SingleSuccess(OperationTypes operation, TModel model = null)
        {
            return new SingleOperationResult<TModel>(operation, true, ServerMessages.OperationCompleted, model);
        }

        protected SingleOperationResult<TModel> SingleSuccess(OperationTypes operation, TModel model, Func<OperationTypes, TModel, bool, string> retrieveMessageFunc)
        {
            var message = retrieveMessageFunc(operation, model, true);
            return new SingleOperationResult<TModel>(operation, true, message, model);
        }

        //collection error

        protected CollectionOperationResult<TModel> CollectionError(OperationTypes operation, PagesList<TModel> models, Func<OperationTypes, IEnumerable<TModel>, bool, string> retrieveMessageFunc)
        {
            var message = retrieveMessageFunc(operation, models, false);
            return new CollectionOperationResult<TModel>(operation, false, message, models);
        }



        /// <summary>
        /// Get operation result with custom messaga for collection
        /// </summary>
        /// <param name="operation">Operation Type</param>
        /// <param name="message">Custom message</param>
        /// <returns>Operation result (Collection/Error)</returns>
        protected CollectionOperationResult<TModel> CollectionError(OperationTypes operation, string message)
        {
            return new CollectionOperationResult<TModel>(operation, false, message);
        }

        /// <summary>
        /// Get operation result with validation errors for colection of records
        /// </summary>
        /// <param name="operation">Operation Type</param>
        /// <param name="models">Collection of models that have been used for operation</param>
        /// <param name="validatinResults">Collection of validation results</param>
        /// <returns>Operation result (Collection/Error)</returns>
        protected CollectionOperationResult<TModel> CollectionError(OperationTypes operation, IEnumerable<TModel> models, IEnumerable<ValidationResult> validatinResults)
        {
            return new CollectionOperationResult<TModel>(operation, false, validatinResults.ToString());
        }

        /// <summary>
        /// Get default operation result with errors for collection
        /// </summary>
        /// <param name="operation">Operation Type</param>
        /// <param name="models">Collection of models that have been used for operation</param>
        /// <returns>Operation result (Collection/Error)</returns>
        protected CollectionOperationResult<TModel> CollectionError(OperationTypes operation, PagesList<TModel> models)
        {
            var message = string.Empty;
            foreach (var model in models)
            {
                message += string.Format("{0} ", SingleError(operation, model));
            }
            return new CollectionOperationResult<TModel>(operation, false, message, models);
        }



        //collection success

        protected CollectionOperationResult<TModel> CollectionSuccess(OperationTypes operation, PagesList<TModel> models, Func<OperationTypes, IEnumerable<TModel>, bool, string> retrieveMessageFunc)
        {
            var message = retrieveMessageFunc(operation, models, true);
            return new CollectionOperationResult<TModel>(operation, true, message, models);
        }

        /// <summary>
        /// Get successful operation result for collection
        /// </summary>
        /// <param name="operation">Operation Type</param>
        /// <param name="models">Collection of models that have been used for operation</param>
        /// <returns>Operation result (Collection/Success)</returns>
        protected CollectionOperationResult<TModel> CollectionSuccess(OperationTypes operation, PagesList<TModel> models)
        {
            return new CollectionOperationResult<TModel>(operation, true, ServerMessages.DataDownloaded, models);
        }

        #endregion

    }
}
