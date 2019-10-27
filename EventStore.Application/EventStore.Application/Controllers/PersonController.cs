﻿using System;
using EventStore.CommonContracts.SourceParameters;
using EventStore.DataContracts.DTO;
using EventStore.Services.Contractors.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using EventStore.Data;
using EventStore.DataContracts;
using EventStore.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EventStore.Api.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/person")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonDataService _personService;
        private readonly IDataMinerService _minerService;
        private readonly IUrlHelper _urlHelper;
        private readonly IDataMinerService _dataMinerService;
        private readonly IServiceProvider provider;

        public PersonController(
            IPersonDataService personService, 
            IUrlHelper uriHelper, 
            IDataMinerService dataMinerService,
            IServiceProvider provider)
        {
            _personService = personService;
            _urlHelper = uriHelper;
            _dataMinerService = dataMinerService;
            this.provider = provider;
        }

        [HttpGet("GetPersons")]
        public IActionResult GetPersons()
        {
            var result = _personService.GetRecords();
            if (!result.WasSuccessful) return NotFound();

            return Ok(result.Records.ToList());
        }

        [HttpPost("AddPersons")]
        public IActionResult AddPersons([FromBody]List<PersonModel> models)
        {
            var result = _personService.AddRange(models);
            if (result.WasSuccessful)
            {
                result.Records.ForEach(model =>
                {
                    _dataMinerService.PostMessage(model);
                });

                return Ok();
            }

            return NotFound();
        }
    }
}