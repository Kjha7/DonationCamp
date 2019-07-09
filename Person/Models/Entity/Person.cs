using System;
using PersonDocument.Models.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace PersonDocument.Models
{
    public class Person
    {

        public Person(PersonCreateRequest personCreateRequest)
        {
            id = Guid.NewGuid();
            firstName = personCreateRequest.firstName;
            graduationDate = personCreateRequest.GraduationDate;
            emailId = personCreateRequest.emailId;
            gender = personCreateRequest.gender;
            createdAt = DateTime.UtcNow;
            password = personCreateRequest.password;
        }

        public static UpdateDefinition<Person> UpdateBuilder(PersonUpdateRequest personUpdateRequest,DateTime updatedAt)
        {
            return Builders<Person>.Update
                .Set(p => p.firstName, personUpdateRequest.firstName)
                .Set(p => p.graduationDate, personUpdateRequest.graduationDate)
                .Set(p => p.emailId, personUpdateRequest.emailId)
                .Set(p => p.gender, personUpdateRequest.gender)
                .Set(p => p.updatedAt, updatedAt);
        }

        [BsonId]
        public Guid id { get; set; }
        public string firstName { get; set; }
        [Required(ErrorMessage ="Fill Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public DateTime graduationDate { get; set; }
        [Required(ErrorMessage = "Fill EmailID")]
        [EmailAddress]
        public string emailId { get; set; }
        public Gender gender { get; set; }
        public DateTime updatedAt { get; set; }
        public DateTime createdAt { get; set; }
        
        public enum Gender
        {
            Male,
            Female,
            Unknown
        }
    }
}