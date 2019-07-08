using System;
using Person.Models.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Person.Models
{
    public class Person
    {

        public Person(PersonCreateRequest personCreateRequest)
        {
            id = Guid.NewGuid();
            firstName = personCreateRequest.firstName;
            lastName = personCreateRequest.lastName;
            emailId = personCreateRequest.emailId;
            gender = personCreateRequest.gender;
            createdAt = DateTime.UtcNow;
        }

        public Person(Person p)
        {
        }

        public static UpdateDefinition<Person> UpdateBuilder(PersonUpdateRequest personUpdateRequest,DateTime updatedAt)
        {
            return Builders<Person>.Update
                .Set(p => p.firstName, personUpdateRequest.firstName)
                .Set(p => p.lastName, personUpdateRequest.lastName)
                .Set(p => p.emailId, personUpdateRequest.emailId)
                .Set(p => p.gender, personUpdateRequest.gender)
                .Set(p => p.updatedAt, updatedAt);
        }

        [BsonId]
        public Guid id { get; set; }
        private string firstName { get; set; }
        private string lastName { get; set; }
        private string emailId { get; set; }
        private Gender gender { get; set; }
        private DateTime updatedAt { get; set; }
        private DateTime createdAt { get; set; }
        
        public enum Gender
        {
            Male,
            Female,
            Unknown
        }
    }
}