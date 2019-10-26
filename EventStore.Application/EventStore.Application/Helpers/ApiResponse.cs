using System;
using BookService.WebApi.Helpers;
using EventStore.CommonContracts.Helpers;
using EventStore.DataContracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Api.Helpers
{
    public class ApiResponse<TModel> where TModel : class, IBaseModel
    {
        public ApiResponse(IUrlHelper uriHelper, string methodName, PagesList<TModel> models, SourceParameters parameters)
        {
            var uriHelper1 = uriHelper;
            PageNumber = models.CurrentPage;
            Models = models;

            NextPage = models.HasNext
                ? new Uri(uriHelper1.Link(methodName, new
                {
                    firstname = parameters.FirstName,
                    lastname = parameters.LastName,
                    personid = parameters.PersonId,
                    companyname = parameters.CompanyName,
                    pageNumber = models.CurrentPage + 1,
                    pageSize = models.PageSize
                }))
                : null;

            PreviousPage = models.HasPrevious
                ? PreviousPage = new Uri(uriHelper1.Link(methodName, new
                {
                    firstname = parameters.FirstName,
                    lastname = parameters.LastName,
                    personid = parameters.PersonId,
                    companyname = parameters.CompanyName,
                    pageNumber = models.CurrentPage - 1,
                    pageSize = models.PageSize
                }))
                : null;

        }
        public PagesList<TModel> Models { get; private set; }
        public int PageNumber { get; private set; }
        public Uri PreviousPage { get; private set; }
        public Uri NextPage { get; private set; }
    }
}
