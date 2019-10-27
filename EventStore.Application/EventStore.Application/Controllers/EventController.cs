using EventStore.Api.Helpers;
using EventStore.CommonContracts.SourceParameters;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventStore.Api.Controllers
{
    [EnableCors("AllowOrigin")]
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

        [HttpGet(Name = "GetEvents")]
        public IActionResult GetEvents(EventSourceParameters parameters)
        {
            var result = _eventService.GetRecords(parameters);
            if (!result.WasSuccessful) return NotFound();

            //TODO
            //var response = new EventApiResponse(_urlHelper, "GetEvents", result.Records, parameters);

            return Ok(result.Records.ToList());
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

            var personModel = new PersonModel
            {
                Id = 1,
                FirstName = "James",
                LastName = "Bond",
                TwitterId = "gfgf",
                CrunchId = "12",
                StartFrom = DateTime.Now
        };

            var per = new PersonModel
            {
                Id = 10,
                FirstName = "Stuart",
                LastName = "Landesberg",
                TwitterId = "Stu_Land",
                CrunchId = "stuart-landesberg",
                StartFrom = DateTime.Now
            };

           // var res = _minerService.PostMessage(personModel);
           // var res1 = _minerService.PostMessage(per);
            //var mes = _minerService.ConsumeMessage();
            //var result = _eventService.GetRecords(parameters);
            //if (!result.WasSuccessful) return NotFound();
            //
            //var response = new EventApiResponse<EventModel>(_urlHelper, "GetEvents", result.Records, parameters);
            return Ok(model);
        }

        public List<EventModel> GetStubedModels()
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

            return new List<EventModel>()
            {
                model
            };
        }
    }
}