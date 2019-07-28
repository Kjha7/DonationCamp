/*
 * [http_method, http_status, http_url, instance, environment]
 * Number of requests came in 
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
using Prometheus;
using System.Diagnostics;

namespace PersonDocument.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonService personServices;
        private CredentialService credentialService;

        private static readonly Histogram LoginDuration
                = Metrics.CreateHistogram("api_response_seconds", "Histogram of services processing durations.",
                    new HistogramConfiguration
                    {
                        LabelNames = new[] { "action" }
                    });

        public PersonController(PersonServices _personServices, CredentialService _credentialService)
        {
            personServices = _personServices;
            credentialService = _credentialService;
        }


        // GET api/person
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var sw = Stopwatch.StartNew();
            ActionResult<IEnumerable<Person>> persons = personServices.GetAllPersons();
            sw.Stop();
            LoginDuration
            .WithLabels("Get_Person")
            .Observe(sw.Elapsed.TotalSeconds);
            return persons;
        }

        // GET api/person/5
        [HttpGet("{id}")]
        public ActionResult<Person> Get(Guid id)
        {
            var sw = Stopwatch.StartNew();
            var person = personServices.GetPerson(id);
            if (person == null) return NotFound();
            sw.Stop();
            LoginDuration
            .WithLabels("Get")
            .Observe(sw.Elapsed.TotalSeconds);
            return person;
        }

        // GET api/person/5
        [HttpGet("donate/{id}")]
        public ActionResult<string> GetPersonTotalDonation(Guid id)
        {
            var sw = Stopwatch.StartNew();
            var task =  personServices.TotalDonation(id.ToString());
            sw.Stop();
            LoginDuration
            .WithLabels("Get_Person_Donation_Total")
            .Observe(sw.Elapsed.TotalSeconds);
            return task.Result;
        }

        // POST api/person
        [HttpPost]
        public ActionResult<string> Post([FromBody] PersonCreateRequest personCreateRequest)
        {
            var sw = Stopwatch.StartNew();
            var person = personServices.CreatePerson(personCreateRequest);
            if(person == null)
            {
                return "Email ID already exist. Please try different ID";
            }
            var credentialCreateRequest = new CredentialsCreateRequest(person);
            credentialService.CreateCredentials(credentialCreateRequest);
            sw.Stop();
            LoginDuration
            .WithLabels("Create_Person")
            .Observe(sw.Elapsed.TotalSeconds);
            return "User Created ID: " + person.PersonId.ToString();
        }

        // PUT api/person/5
        [HttpPut("{id}")]
        public ActionResult<Person> Put(Guid id, [FromBody] PersonUpdateRequest personUpdateRequest)
        {
            var sw = Stopwatch.StartNew();
            var person = personServices.UpdatePerson(id, personUpdateRequest);
            sw.Stop();
            LoginDuration
            .WithLabels("Edit_Person")
            .Observe(sw.Elapsed.TotalSeconds);
            return person;
        }

        // DELETE api/person/5
        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id)
        {
            var sw = Stopwatch.StartNew();
            var response = personServices.DeletePersonAsync(id);
            _ = credentialService.DeleteCredentialsAsync(id);
            sw.Stop();
            LoginDuration
            .WithLabels("Delete_Person")
            .Observe(sw.Elapsed.TotalSeconds);
            return response;
        }
    }
}
