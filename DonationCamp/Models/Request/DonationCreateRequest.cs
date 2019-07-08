using System;
using DonationCamp.Models.Entity;

namespace DonationCamp.Models.Request
{
    public class DonationCreateRequest
    {
        public PersonDocument.Models.Person Donar { get; set; }
        public Types Category { get; set; }
    }
}
