using System;
using System.Collections.Generic;
using System.Linq;
using BookService.WebApi.Helpers;
using EventStore.CommonContracts.Helpers;
using EventStore.Data.Entities;
using EventStore.DataContracts;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors;
using EventStore.Services.Contractors.Interfaces;
using EventStore.Services.Contractors.Utils;

namespace EventStore.Services.Services
{
    public class EventDataService : DataService<EventModel, EventEntity>, IEventDataService
    {
        public EventDataService(IEventStoreRepository<EventModel, EventEntity> repository)
            : base(repository)
        {
        }


        public SingleOperationResult<EventModel> Add(EventModel newItem)
        {
            return null;
        }

        public CollectionOperationResult<EventModel> AddRange(IEnumerable<EventModel> items)
        {
            return null;
        }

        public SingleOperationResult<EventModel> Update(EventModel item)
        {
            return null;
        }

        public CollectionOperationResult<EventModel> UpdateRange(IEnumerable<EventModel> items)
        {
            return null;
        }

        public SingleOperationResult<EventModel> RemoveById(int id)
        {
            return null;
        }

        public SingleOperationResult<EventModel> SaveChanges(EventModel model, OperationTypes operation)
        {
            return null;
        }

        public IEnumerable<SingleOperationResult<EventModel>> SaveChanges(IEnumerable<EventModel> models, OperationTypes operation)
        {
            yield break;
        }

        public SingleOperationResult<EventModel> GetSingleRecord(int id)
        {
            return null;
        }

        public SingleOperationResult<EventModel> GetSingleRecordBySource(int sourceId, int id)
        {
            return null;
        }

        public CollectionOperationResult<EventModel> GetRecords(SourceParameters parameters)
        {
            try
            {
                var rawModels = parameters.PersonId != 0 
                    ? Repository.GetAsync(i => i.Id == parameters.PersonId).Result 
                    : Repository.GetAsync().Result;

                var orderedModels = rawModels.OrderByDescending(model => model.EventDate).AsQueryable();

                var pagedModels = PagesList<EventModel>.Init(orderedModels, parameters.PageNumber, parameters.PageSize);

                return CollectionSuccess(OperationTypes.Read, pagedModels);
            }
            catch (Exception e)
            {
                return CollectionError(OperationTypes.Read, ServerMessages.CannotLoadRecords);
            }
        }
    }
}
