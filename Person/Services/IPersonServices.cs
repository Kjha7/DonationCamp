using System;
using System.Collections.Generic;
using Person.Models.Request;

namespace Person.Models
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