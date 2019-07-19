﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using DonationCamp.Models.Response;
using DonationCamp.Services;
using Microsoft.AspNetCore.Mvc;
using PersonDocument.Models;
using PersonDocument.Models.Entity;
using PersonDocument.Models.Request;
using PersonDocument.Services;
using System.Threading.Tasks;

namespace PersonDocument.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private IPersonService personServices;
        private CredentialService credentialService;
        public PersonController(PersonServices _personServices, CredentialService _credentialService)
        {
            personServices = _personServices;
            credentialService = _credentialService;
        }


        // GET api/person
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            return personServices.GetAllPersons();
        }

        // GET api/person/5
        [HttpGet("{id}")]
        public ActionResult<Person> Get(Guid id)
        {
            var person = personServices.GetPerson(id);
            if (person == null) return NotFound();
            return person;
        }

        // GET api/person/5
        [HttpGet("donate/{id}")]
        public ActionResult<string> GetTotalDonation(Guid id)
        {
            var task =  personServices.TotalDonation(id.ToString());
            return task.Result;
        }

        // POST api/person
        [HttpPost]
        public ActionResult<Person> Post([FromBody] PersonCreateRequest personCreateRequest)
        {
            var person = personServices.CreatePerson(personCreateRequest);
            var credentialCreateRequest = new CredentialsCreateRequest(person);
            credentialService.CreateCredentials(credentialCreateRequest);
            return person;
        }

        // PUT api/person/5
        [HttpPut("{id}")]
        public ActionResult<Person> Put(Guid id, [FromBody] PersonUpdateRequest personUpdateRequest)
        {
            return personServices.UpdatePerson(id, personUpdateRequest);
        }

        // DELETE api/person/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            personServices.DeletePerson(id);
            credentialService.DeleteCredentials(id);
        }
    }
}
