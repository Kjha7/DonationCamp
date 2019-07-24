using System;
namespace DonationCamp.Models.Response
{
    public class SessionResponse
    {

        public SessionResponse(string id, Guid personID)
        {
            SessionId = id;
            PersonId = personID;
        }

        public string SessionId { get; set; }
        public Guid PersonId { get; set; }
    }
}
