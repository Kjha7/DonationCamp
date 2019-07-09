using System;
using System.Collections.Generic;
using PersonDocument.Models;
using PersonDocument.Models.Request;

namespace PersonDocument.Services
{
    public interface IPersonService
    {
        List<Person> GetAllPersons();

        Person GetPerson(Guid personId);

        void DeletePerson(Guid personId);

        Person CreatePerson(PersonCreateRequest personCreateRequest);

        Person UpdatePerson(Guid personId, PersonUpdateRequest personUpdateRequest);
    }
}