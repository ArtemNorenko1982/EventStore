using BookService.WebApi.Helpers;
using EventStore.Api.Helpers;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Api.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetEvents(SourceParameters parameters)
        {
            var result = _eventService.GetRecords(parameters);
            if (!result.WasSuccessful) return NotFound();

            var response = new ApiResponse<EventModel>(_urlHelper, "GetEvents", result.Records, parameters);
            return Ok(response);
        }
    }
}