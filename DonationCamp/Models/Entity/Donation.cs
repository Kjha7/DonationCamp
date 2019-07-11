using System;
using DonationCamp.Models.Request;
using DonationCamp.Models.Response;
using MongoDB.Bson.Serialization.Attributes;

namespace DonationCamp.Models.Entity
{
    public class Donation
    {
        public Donation(DonationCreateRequest donationCreateRequest,string personId)
        {
            Id = Guid.NewGuid();
            Donar = personId;
            Category = donationCreateRequest.Category;
            dateTime = DateTime.UtcNow;
            Amt = donationCreateRequest.Amt;
        }

        [BsonId]
        public Guid Id { get; set; }
        public string Donar { get; set; }
        public Types? Category { get; set; }
        public DateTime? dateTime { get; set; }
        public int? Amt { get; set; }
    }


    public enum Types
    {
        Anything,
        Athletics,
        Education,
        Infrastructure
    }
}
