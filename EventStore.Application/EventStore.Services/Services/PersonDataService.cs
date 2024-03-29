﻿using EventStore.CommonContracts.Helpers;
using EventStore.Data;
using EventStore.DataContracts;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors;
using EventStore.Services.Contractors.Interfaces;
using EventStore.Services.Contractors.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventStore.Services.Services
{
    public class PersonDataService : DataService<PersonModel, PersonEntity>, IPersonDataService
    {
        public PersonDataService(IEventStoreRepository<PersonModel, PersonEntity> repository) : base(repository)
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

        public CollectionOperationResult<PersonModel> AddRange(IEnumerable<PersonModel> models)
        {
            try
            {
                var result = Repository.AddAsync(models).Result.ToList();

                if (!result.Any()) return CollectionError(OperationTypes.Add, ServerMessages.ErrorInDataBase);
                result.ForEach(model => model.StartFrom = DateTime.Now.AddDays(-7).ToString("yyyy-MM-ddTHH\\:mm\\:sszzz"));
                return CollectionSuccess(OperationTypes.Add, ToPagesList(result));

            }
            catch (Exception e)
            {
                return CollectionError(OperationTypes.Add, ServerMessages.CannotPerformOperation);
            }
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


        public CollectionOperationResult<PersonModel> GetRecords()
        {
            try
            {
                var result = Repository.GetAsync(i => i.Id != 0).Result;
                //var result = Repository.GetAsync(model =>
                //        (parameters.PersonIds.Any() || parameters.PersonIds.Contains(model.Id)) &&
                //        (String.IsNullOrEmpty(parameters.CompanyName) ||
                //         String.Equals(parameters.CompanyName, model.CompanyName, StringComparison.InvariantCultureIgnoreCase)) &&
                //        (String.IsNullOrEmpty(parameters.FirstName) || 
                //         String.Equals(parameters.FirstName, model.FirstName, StringComparison.InvariantCultureIgnoreCase)) &&
                //        (String.IsNullOrEmpty(parameters.LastName) || 
                //         String.Equals(parameters.LastName, model.LastName, StringComparison.InvariantCultureIgnoreCase)))
                //    .Result;

                return CollectionSuccess(OperationTypes.Read, ToPagesList(result.ToList()));
            }
            catch (Exception e)
            {
                return CollectionError(OperationTypes.Read, ServerMessages.CannotPerformOperation);
            }
        }

        private PagesList<PersonModel> ToPagesList(List<PersonModel> result)
        {
            //TODO
            return PagesList<PersonModel>.Init(result, 1, result.Count());
        }
    }
}
