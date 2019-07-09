using System;
using Donation.Configs;
using Donation.Models.Entity;
using Donation.Models.Request;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace Donation.Services
{
    public class DonationServices : IDonationServices
    {
        public MongoDbConfig _donation;
        public LoginConfig _loginconfig;

        public IMongoCollection<DonationCamp> dontaions;
        public IMongoCollection<Credentials> credentials;
        public DonationServices(IOptions<MongoDbConfig> settings)
        {
            _donation = settings.Value;
            var client = new MongoClient(_donation.Uri);
            var database = client.GetDatabase(_donation.Database);

            dontaions = database.GetCollection<DonationCamp>(_donation.Collection);
        }

        public DonationServices(IOptions<LoginConfig> settings)
        {
            _loginconfig = settings.Value;
            var client = new MongoClient(_loginconfig.Uri);
            var database = client.GetDatabase(_loginconfig.Database);

            credentials = database.GetCollection<Credentials>(_loginconfig.Collection);
        }

        public DonationCamp donate(DonationCreateRequest donationCreateRequest)
        {
            var donar = new DonationCamp(donationCreateRequest);
            dontaions.InsertOneAsync(donar).Wait();

            return donar;
        }

        public string login(Credentials credentials)
        {
            return "fd";
        }
    }
}
