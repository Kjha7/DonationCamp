using System;
using Donation.Models.Entity;

namespace Donation.Models.Request
{
    public class DonationCreateRequest
    {
        public Types Category { get; set; }
        public DateTime dateTime { get; set; }
    }
}
