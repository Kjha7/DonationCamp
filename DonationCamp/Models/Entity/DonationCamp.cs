using System;
using PersonDocument;
using DonationCamp.Models.Request;
using MongoDB.Bson.Serialization.Attributes;

namespace DonationCamp.Models.Entity
{
    public class DonationCamp
    {
        public DonationCamp(DonationCreateRequest donationCreateRequest)
        {
            Id = Guid.NewGuid();
            Donar = donationCreateRequest.Donar;
            Category = donationCreateRequest.Category;
        }

        [BsonId]
        public Guid Id { get; set; }
        public PersonDocument.Models.Person Donar { get; set; }
        public Types Category { get; set; }
    }

    public enum Types
    {
        Anything,
        Athletics,
        Education,
        Infrastructure
    }
}
