using System;
using DonationCamp.Configs;
using DonationCamp.Models.Entity;
using DonationCamp.Models.Request;
using System.Web;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;
using Prometheus;

namespace DonationCamp.Services
{
    public class SessionServices
	{
		private readonly string host = Environment.MachineName;
		static readonly Counter UserSessionCounter = Metrics.CreateCounter("UserSessions", "Count", "host", "status");
		static readonly Gauge UserSessionGauge = Metrics.CreateGauge("UserActiveSessions", "Count", "host");
		private LoginConfig _config;
        public IMongoCollection<Credentials> credentials;
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
            var donar = credentials.Find(p => p.EmailId == loginRequest.EmailId && p.Password == loginRequest.Password).FirstOrDefault();
            if (donar != null)
			{
                UserSessionGauge.Inc();
                UserSessionCounter.WithLabels(host, "Login").Inc();
				return donar.PersonId;
            }
            return donar.PersonId;
        }
    }
}
