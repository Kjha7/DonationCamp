using System;
using PersonDocument;
using Donation.Models.Request;
using MongoDB.Bson.Serialization.Attributes;

namespace Donation.Models.Entity
{
    public class DonationCamp
    {
        public DonationCamp(DonationCreateRequest donationCreateRequest)
        {
            Id = Guid.NewGuid();
            Donar = donationCreateRequest.Donar;
            Category = donationCreateRequest.Category;
            dateTime = donationCreateRequest.dateTime;
        }

        [BsonId]
        public Guid Id { get; set; }
        public PersonDocument.Models.Person Donar { get; set; }
        public Types Category { get; set; }
        public DateTime dateTime { get; set; }
    }


    public enum Types
    {
        Anything,
        Athletics,
        Education,
        Infrastructure
    }
}
