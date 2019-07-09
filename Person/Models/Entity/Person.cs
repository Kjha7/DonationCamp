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
            PersonId = Guid.NewGuid();
            FirstName = personCreateRequest.FirstName;
            GraduationDate = personCreateRequest.GraduationDate;
            EmailId = personCreateRequest.EmailId;
            gender = personCreateRequest.gender;
            CreatedAt = DateTime.UtcNow;
            Password = personCreateRequest.Password;
        }

        public static UpdateDefinition<Person> UpdateBuilder(PersonUpdateRequest personUpdateRequest,DateTime updatedAt)
        {
            return Builders<Person>.Update
                .Set(p => p.FirstName, personUpdateRequest.FirstName)
                .Set(p => p.GraduationDate, personUpdateRequest.GraduationDate)
                .Set(p => p.EmailId, personUpdateRequest.EmailId)
                .Set(p => p.gender, personUpdateRequest.gender)
                .Set(p => p.UpdatedAt, updatedAt);
        }

        [BsonId]
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Fill Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public DateTime? GraduationDate { get; set; }
        [Required(ErrorMessage = "Fill EmailID")]
        [EmailAddress]
        public string EmailId { get; set; }
        public Gender gender { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public enum Gender
        {
            Male,
            Female,
            Unknown
        }
    }
}