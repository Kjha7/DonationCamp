using System;
using Donation.Models.Entity;
using Donation.Models.Request;

namespace Donation.Services
{
    public interface IDonationServices
    {
        Credentials login(Credentials credentials);
        DonationCamp donate(DonationCreateRequest donationCreateRequest);
    }
}
