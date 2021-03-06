﻿using System;
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
		static readonly Counter UserSessionCounter = Metrics.CreateCounter("Users_Login", "Total users login Count", "emailID");
		static readonly Gauge UserSessionGauge = Metrics.CreateGauge("Users_Login_Count", "Total active sessions", "emailID");
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
                UserSessionGauge.WithLabels(donar.EmailId).Inc();
                UserSessionCounter.WithLabels(donar.EmailId).Inc();
				return donar.PersonId;
            }
            return donar.PersonId;
        }
    }
}
