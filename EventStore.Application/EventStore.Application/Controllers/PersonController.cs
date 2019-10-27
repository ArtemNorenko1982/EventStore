using EventStore.CommonContracts.SourceParameters;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EventStore.Api.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/person/")]
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
        public IActionResult GetPersons()
        {
            var result = _personService.GetRecords(new PersonSourceParameters());
            if (!result.WasSuccessful) return NotFound();

            return Ok(result.Records.ToList());
        }

        [HttpPost]
        public IActionResult AddPersons(IEnumerable<PersonModel> models)
        {
            var result = _personService.AddRange(models);
            if (result.WasSuccessful)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}