using System;
using DonationCamp.Models.Entity;
using DonationCamp.Models.Response;

namespace DonationCamp.Models.Request
{
    public class DonationCreateRequest
    {
        public Types Category { get; set; }
        public int? Amt { get; set; }
    }
}
