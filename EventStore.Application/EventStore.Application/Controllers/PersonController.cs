using BookService.WebApi.Helpers;
using EventStore.Api.Helpers;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventStore.Api.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonDataService _personService;
        private readonly IUrlHelper _urlHelper;

        public PersonController(IPersonDataService personService, IUrlHelper uriHelper)
        {
            _personService = personService;
            _urlHelper = uriHelper;
        }

        [HttpGet(Name = "GetPersons")]
        public IActionResult GetPersons(SourceParameters parameters)
        {
            var result = _personService.GetRecords(parameters);
            if (!result.WasSuccessful) return NotFound();

            var response = new ApiResponse<PersonModel>(_urlHelper, "GetPersons", result.Records, parameters);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddPerson(PersonModel model)
        {
            var result = _personService.Add(model);
            if (result.WasSuccessful)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}