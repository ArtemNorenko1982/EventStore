using System;
using BookService.WebApi.Helpers;
using EventStore.Api.Helpers;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Api.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventDataService _eventService;
        private readonly IUrlHelper _urlHelper;

        public EventController(IEventDataService eventService, IUrlHelper uriHelper)
        {
            _eventService = eventService;
            _urlHelper = uriHelper;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetEvent(int id)
        {
            var model = new EventModel
            {
                PersonId = 1,
                EventDate = DateTime.Now,
                Id = 1,
                ActionType = "Post",
                Content =
                    "The James Bond series focuses on a fictional British Secret Service agent created in 1953 by writer Ian Fleming, who featured him in twelve novels and two short-story collections. Since Fleming's death in 1964, eight other authors have written authorised Bond novels or novelizations: Kingsley Amis, Christopher Wood, John Gardner, Raymond Benson, Sebastian Faulks, Jeffery Deaver, William Boyd and Anthony Horowitz. The latest novel is Forever and a Day by Anthony Horowitz, published in May 2018. Additionally Charlie Higson wrote a series on a young James Bond, and Kate Westbrook wrote three novels based on the diaries of a recurring series character, Moneypenny.",
                Producer = "twitter",
                Source = "https://en.wikipedia.org/wiki/James_Bond"
            };
            
            //var result = _eventService.GetRecords(parameters);
            //if (!result.WasSuccessful) return NotFound();
            //
            //var response = new ApiResponse<EventModel>(_urlHelper, "GetEvents", result.Records, parameters);
            return Ok(model);
        }
    }
}