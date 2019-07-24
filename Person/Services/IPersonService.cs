using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonDocument.Models;
using PersonDocument.Models.Request;

namespace PersonDocument.Services
{
    public interface IPersonService
    {
        List<Person> GetAllPersons();

        Person GetPerson(Guid personId);

        Task<bool> DeletePersonAsync(Guid personId);

        Person CreatePerson(PersonCreateRequest personCreateRequest);

        Person UpdatePerson(Guid personId, PersonUpdateRequest personUpdateRequest);
        Task<string> TotalDonation(string v);
        bool IsUserAvailable(string userEmailId);
    }
}