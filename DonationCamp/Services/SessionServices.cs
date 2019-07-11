using System;
using DonationCamp.Configs;
using DonationCamp.Models.Entity;
using DonationCamp.Models.Request;
using System.Web;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;

namespace DonationCamp.Services
{
    public class SessionServices
    {
        private LoginConfig _config;
        public IMongoCollection<Credentials> credentials;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionServices(IOptions<LoginConfig> settings, IHttpContextAccessor httpContextAccessor)
        {
            _config = settings.Value;
            var client = new MongoClient(_config.Uri);
            var database = client.GetDatabase(_config.Database);
            credentials = database.GetCollection<Credentials>(_config.Collection);
        }

        public Guid Login(LoginRequest loginRequest)
        {
            //var emailID = await credentials.Find(p => p.EmailId == loginRequest.emailId).SingleAsync().Result;
        var donar = credentials.Find(p => p.EmailId == loginRequest.emailId && p.Password == loginRequest.password).FirstOrDefault();
            if (donar != null)
            {
                return donar.PersonId;
            }
            return donar.PersonId;
        }

    }
}
