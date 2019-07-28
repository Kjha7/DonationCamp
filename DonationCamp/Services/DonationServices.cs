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
        private Counter DonationCounter = Metrics.CreateCounter("Person_donation_count", "Total times user donated", "User");
        private Gauge DonationGauge = Metrics.CreateGauge("Person_Total_Donation", "Person total donation", "User");
        private Gauge TotalDonationGauge = Metrics.CreateGauge("Total_Donation", "Total donation Count");
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
                var donation = new Donation(donationCreateRequest, personId);
                donations.InsertOneAsync(donation).Wait();
                DonationCounter.WithLabels(personId).Inc();
                double gaugeval = DonationGauge.WithLabels(personId).Value;
                double totalDonation = 0;
                IEnumerable<string[]> gaugelabels = DonationGauge.GetAllLabelValues();
                foreach (var label in gaugelabels)
                {
                    totalDonation += DonationGauge.WithLabels(label).Value;
                }
                DonationGauge.WithLabels(personId).Set((double)donationCreateRequest.Amt + gaugeval);
                TotalDonationGauge.Set(totalDonation + (double)donationCreateRequest.Amt);
                // DonationCounter.WithLabels(host, "donate").Inc();
                //
                return donation;
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
            var TotalDonation = donations.Find(Donation => true).ToList();
            int totalDonation = 0;
            foreach (Donation person in TotalDonation)
            {
                totalDonation += (int)person.Amt;
            }
            TotalDonationGauge.Set(totalDonation);
            return TotalDonation;
        }
    }
}