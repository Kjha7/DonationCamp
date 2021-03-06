using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonDocument.Configs;
using PersonDocument.Models;
using PersonDocument.Models.Entity;
using PersonDocument.Models.Request;
using Prometheus;


namespace PersonDocument.Services
{
    public class PersonServices : IPersonService
    {
        private string host = Environment.MachineName;
        //private static readonly Counter UserCounter = Metrics.CreateCounter("useraccountstatus", "Count", "host", "status");
        private static readonly Counter UserPostCounter = Metrics.CreateCounter("total_users_registered", "Total Count of Users registered");
        private static readonly Gauge UserGauge = Metrics.CreateGauge("total_users_account", "Count of users account in system");
        private MongoDbConfig _config;
        private IMongoCollection<Person> _person;
        private readonly IHttpClientFactory httpClientFactory;

        public PersonServices(IOptions<MongoDbConfig> settings, IHttpClientFactory _httpClientFactory)
        {
            _config = settings.Value;
            var client = new MongoClient(_config.Uri);
            var database = client.GetDatabase(_config.Database);
            _person = database.GetCollection<Person>(_config.Collection);

            httpClientFactory = _httpClientFactory;
        }

        public List<Person> GetAllPersons()
        {
            return _person.Find(Person => true).ToList();
        }

        public Person GetPerson(Guid personId)
        {
            try
            {
                var person = _person.Find(p => p.PersonId == personId).FirstOrDefault();
                return person;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletePersonAsync(Guid personId)
        {
            try
            {
                DeleteResult actionResult = await _person.DeleteOneAsync(Builders<Person>.Filter.Eq("PersonId", personId));
                //UserGauge.WithLabels(host).Dec();
                UserGauge.Dec();
                return actionResult.IsAcknowledged;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Person CreatePerson(PersonCreateRequest personCreateRequest)
        {
            try
            {
                var personDocument = new Person(personCreateRequest);
                if (!IsUserAvailable(personDocument.EmailId)) { return null; }
                _person.InsertOneAsync(personDocument).Wait();

                var credentials = new Credentials();
                //UserGauge.WithLabels(host).Inc();
                UserPostCounter.Inc();
                UserGauge.Inc();
                return personDocument;
            }
            catch
            {
                System.Console.WriteLine("Create person service failed");
                throw;
            }
        }

        public Person UpdatePerson(Guid personId, PersonUpdateRequest personUpdateRequest)
        {
            DateTime updatedAt = DateTime.UtcNow;
            return _person.FindOneAndUpdateAsync(p => p.PersonId == personId,
                 Person.UpdateBuilder(personUpdateRequest, updatedAt)).Result;
        }

        public async Task<string> TotalDonation(string id)
        {
            try
            {
                string url = "http://donationcamp/r/donate/" + id;
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                var client = httpClientFactory.CreateClient();

                //HttpClientHandler httpClientHandler = new HttpClientHandler();
                //httpClientHandler.
                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch
            {
                System.Console.WriteLine("Total Donation service failed");
                throw;
            }

        }

        //Check for dublicate email ID
        public bool IsUserAvailable(string userEmailId)
        {
            var person = _person.Find(p => p.EmailId == userEmailId).FirstOrDefault();
            if (person == null)
                return true;

            return false;
        }
    }
}