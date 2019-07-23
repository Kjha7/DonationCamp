using System;
using System.Collections.Generic;
using DonationCamp.Configs;
using DonationCamp.Models.Entity;
using DonationCamp.Models.Request;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Prometheus;

namespace DonationCamp.Services
{
    public class DonationServices
    {
        private string host = Environment.MachineName;
        private Counter SessionCounter = Metrics.CreateCounter("UserSessionStatus", "Count", "host", "status");
        private Gauge SessionGauge = Metrics.CreateGauge("ActiveSessions", "Count", "host");
        public MongoDbConfig _donation;
        public IMongoCollection<Donation> donations;

        public DonationServices(IOptions<MongoDbConfig> settings)
        {
            _donation = settings.Value;
            var client = new MongoClient(_donation.Uri);
            var database = client.GetDatabase(_donation.Database);
            donations = database.GetCollection<Donation>(_donation.Collection);
        }

        public Donation Donate(DonationCreateRequest donationCreateRequest, string personId)
        {
            try
            {
                var donar = new Donation(donationCreateRequest, personId);
                donations.InsertOneAsync(donar).Wait();
                SessionGauge.WithLabels(host).Inc();
                SessionCounter.WithLabels(host, "donate").Inc();
                return donar;
            }
            catch (Exception)
            {
                System.Console.WriteLine("Donation service failed");
                throw;
            }
        }

        public string PersonTotalDonation(string personId)
        {
            try
            {
                var donation = donations.Find(p => p.Donar == personId).ToList();
                int totalDonation = 0;
                foreach (Donation person in donation)
                {
                    totalDonation += (int)person.Amt;
                }

                return totalDonation.ToString();
            }
            catch (Exception)
            {
                System.Console.WriteLine("Can not find donar.");
                throw;
            }
        }

         public List<Donation> GetAllDonation()
        {
            return donations.Find(Donation => true).ToList();
        }
    }
}