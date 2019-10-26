using System;
using System.Collections.Generic;
using BookService.WebApi.Helpers;
using EventStore.Data;
using EventStore.DataContracts;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors;
using EventStore.Services.Contractors.Interfaces;
using EventStore.Services.Contractors.Utils;

namespace EventStore.Services.Services
{
    public class PersonDataService : DataService<PersonModel, PersonEntity>, IPersonDataService
    {
        protected PersonDataService(IEventStoreRepository<PersonModel, PersonEntity> repository) : base(repository)
        {
        }


        public SingleOperationResult<PersonModel> Add(PersonModel model)
        {
            try
            {
                var result = Repository.AddAsync(model).Result;

                return SingleSuccess(OperationTypes.Add, result);
            }
            catch (Exception e)
            {
                return SingleError(OperationTypes.Add, ServerMessages.CannotPerformOperation);
            }
        }

        public CollectionOperationResult<PersonModel> AddRange(IEnumerable<PersonModel> items)
        {
            return null;
        }

        public SingleOperationResult<PersonModel> Update(PersonModel item)
        {
            return null;
        }

        public CollectionOperationResult<PersonModel> UpdateRange(IEnumerable<PersonModel> items)
        {
            return null;
        }

        public SingleOperationResult<PersonModel> RemoveById(int id)
        {
            return null;
        }

        public SingleOperationResult<PersonModel> SaveChanges(PersonModel model, OperationTypes operation)
        {
            return null;
        }

        public IEnumerable<SingleOperationResult<PersonModel>> SaveChanges(IEnumerable<PersonModel> models, OperationTypes operation)
        {
            yield break;
        }

        public SingleOperationResult<PersonModel> GetSingleRecord(int id)
        {
            return null;
        }

        public SingleOperationResult<PersonModel> GetSingleRecordBySource(int sourceId, int id)
        {
            return null;
        }

        public CollectionOperationResult<PersonModel> GetRecords(SourceParameters parameters)
        {
            return null;
        }
    }
}
