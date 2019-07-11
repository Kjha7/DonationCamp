using System;
using DonationCamp.Models.Response;
using MongoDB.Bson.Serialization.Attributes;

namespace DonationCamp.Models.Entity
{
    [BsonIgnoreExtraElements]
    public class Credentials
    {
        public Credentials(DonarResponse donarResponse)
        {
            PersonId = donarResponse.PersonId;
            EmailId = donarResponse.EmailId;
            Password = donarResponse.Password;
        }
        public Credentials() { }
        public Guid PersonId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
