using System;
using MongoDB.Bson.Serialization.Attributes;
using PersonDocument.Models.Request;

namespace PersonDocument.Models.Entity
{
    public class Credentials
    {
        public Credentials(CredentialsCreateRequest credentialsCreateRequest)
        {
            PersonId = credentialsCreateRequest.PersonId;
            EmailId = credentialsCreateRequest.EmailId;
            Password = credentialsCreateRequest.Password;
        }
        public Credentials() { }


        public Guid PersonId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
