using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonDocument.Configs;
using PersonDocument.Models;
using PersonDocument.Models.Entity;
using PersonDocument.Models.Request;


namespace PersonDocument.Services
{
    public class PersonServices :IPersonService
    {
        private MongoDbConfig _config;

        private IMongoCollection<Person> _person;

        public PersonServices(IOptions<MongoDbConfig> settings)
        {
            _config = settings.Value;
            var client = new MongoClient(_config.Uri);
            var database = client.GetDatabase(_config.Database);

            _person = database.GetCollection<Person>(_config.Collection);
        }

        public List<Person> GetAllPersons()
        {
            return _person.Find(Person => true).ToList();
        }

        public Person GetPerson(Guid personId)
        {
            try
            {
                var person = _person.Find(p => p.PersonId == personId).SingleAsync().Result;
                return person;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeletePerson(Guid personId)
        {
            try
            {
                _person.DeleteOneAsync(personId.ToString()).Wait();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Person CreatePerson(PersonCreateRequest personCreateRequest)
        {
            var personDocument = new Person(personCreateRequest);
            _person.InsertOneAsync(personDocument).Wait();

            var credentials = new Credentials();

            return personDocument;
        }

        public Person UpdatePerson(Guid personId, PersonUpdateRequest personUpdateRequest)
        {
            DateTime updatedAt = DateTime.UtcNow;
            return _person.FindOneAndUpdateAsync(p => p.PersonId == personId,
                 Person.UpdateBuilder(personUpdateRequest, updatedAt)).Result;
            

        }
    }
}