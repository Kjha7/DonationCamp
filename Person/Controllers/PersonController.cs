using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PersonDocument.Models;
using PersonDocument.Models.Request;

namespace PersonDocument.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        IPersonService personServices;
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Models.Person>> Get()
        {
            return personServices.GetAllPersons();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Models.Person> Get(Guid id)
        {
            var person = personServices.GetPerson(id);
            if (person == null) return NotFound();
            return person;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Models.Person> Post([FromBody] PersonCreateRequest personCreateRequest)
        {
            return personServices.CreatePerson(personCreateRequest);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Models.Person> Put(Guid id, [FromBody] PersonUpdateRequest personUpdateRequest)
        {
            return personServices.UpdatePerson(id, personUpdateRequest);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            personServices.DeletePerson(id);
        }
    }
}
