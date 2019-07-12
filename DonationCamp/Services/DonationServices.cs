using System;
using DonationCamp.Configs;
using DonationCamp.Models.Entity;
using DonationCamp.Models.Request;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DonationCamp.Services
{
    public class DonationServices
    {
        public MongoDbConfig _donation;
        public IMongoCollection<Donation> dontaions;

        public DonationServices(IOptions<MongoDbConfig> settings)
        {
            _donation = settings.Value;
            var client = new MongoClient(_donation.Uri);
            var database = client.GetDatabase(_donation.Database);
            dontaions = database.GetCollection<Donation>(_donation.Collection);
        }


        public Donation Donate(DonationCreateRequest donationCreateRequest, string personId)
        {
            var donar = new Donation(donationCreateRequest, personId);
            dontaions.InsertOneAsync(donar).Wait();
            return donar;
        }

        

    }
}
