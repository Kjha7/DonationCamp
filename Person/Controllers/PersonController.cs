using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Person.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        IPersonServices personServices;
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return personServices.GetAllPersons();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return personServices.GetPerson();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] PersonCreateRequest personCreateRequest)
        {
            return personServices.CreatePerson(personCreateRequest);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PersonUpdateRequest personUpdateRequest)
        {
            return personServices.UpdatePerson(id, personUpdateRequest);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            return personServices.DeletePerson(id);
        }
    }
}
