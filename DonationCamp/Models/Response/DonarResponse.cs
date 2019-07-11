using System;
using DonationCamp.Models.Entity;

namespace DonationCamp.Models.Response
{
    public class DonarResponse
    {

        public string EmailId { get; set; }
        public string Password { get; set; }
        public Guid PersonId { get; set; }
    }
}
