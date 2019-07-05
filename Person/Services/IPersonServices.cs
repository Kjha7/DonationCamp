using System;
using System.Collections.Generic;
using MongoDB.Driver;
using Person.Models.Requests;


namespace DefaultNamespace
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