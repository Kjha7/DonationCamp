/*
 * Number of user registered per day/week/month [counter]
 * Process time per service [Histogram]
 * Total users [gauge]
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using PersonDocument.Models;
using PersonDocument.Models.Request;
using PersonDocument.Services;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MongoDB.Bson;

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
        public ActionResult<string> GetPersonTotalDonation(Guid id)
        {
            var task =  personServices.TotalDonation(id.ToString());
            return task.Result;
        }

        // POST api/person
        [HttpPost]
        public ActionResult<string> Post([FromBody] PersonCreateRequest personCreateRequest)
        {
            var person = personServices.CreatePerson(personCreateRequest);
            if(person == null)
            {
                return "Email ID already exist. Please try different ID";
            }
            var credentialCreateRequest = new CredentialsCreateRequest(person);
            credentialService.CreateCredentials(credentialCreateRequest);
            return "User Created ID: " + person.PersonId.ToString();
        }

        // PUT api/person/5
        [HttpPut("{id}")]
        public ActionResult<Person> Put(Guid id, [FromBody] PersonUpdateRequest personUpdateRequest)
        {
            return personServices.UpdatePerson(id, personUpdateRequest);
        }

        // DELETE api/person/5
        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id)
        {
            var response = personServices.DeletePersonAsync(id);
            var response2 = credentialService.DeleteCredentialsAsync(id);
            return response;
        }
    }
}
