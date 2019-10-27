using EventStore.CommonContracts.Helpers;
using EventStore.CommonContracts.SourceParameters;
using EventStore.DataContracts.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EventStore.Api.Helpers
{
    public class EventApiResponse
    {
        public EventApiResponse(IUrlHelper uriHelper, string methodName, PagesList<EventModel> models, EventSourceParameters parameters)
        {
            var uriHelper1 = uriHelper;
            PageNumber = models.CurrentPage;
            Models = models;

            NextPage = models.HasNext
                ? new Uri(uriHelper1.Link(methodName, new
                {
                    keyPhrase = parameters.KeyPhrase,
                    personids = parameters.PersonIds,
                    pageNumber = models.CurrentPage + 1,
                    pageSize = models.PageSize
                }))
                : null;

            PreviousPage = models.HasPrevious
                ? PreviousPage = new Uri(uriHelper1.Link(methodName, new
                {
                    keyPhrase = parameters.KeyPhrase,
                    personids = parameters.PersonIds,
                    pageNumber = models.CurrentPage - 1,
                    pageSize = models.PageSize
                }))
                : null;

        }
        public PagesList<EventModel> Models { get; private set; }
        public int PageNumber { get; private set; }
        public Uri PreviousPage { get; private set; }
        public Uri NextPage { get; private set; }
    }
}
