using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DonationCamp.Services;
using DonationCamp.Models.Request;
using Microsoft.AspNetCore.Http;
using DonationCamp.Models.Entity;

namespace DonationCamp.Controllers
{
    [Route("/")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        public DonationServices donationServices;
        public SessionServices sessionServices;
        public DonationController(DonationServices donation, SessionServices _sessionServices)
        {
            donationServices = donation;
            sessionServices = _sessionServices;
        }

        // POST account/Login
        [HttpPost]
        [Route("account/Login")]
        public string Login([FromBody]LoginRequest loginRequest)
        {
            // Require the user to have a confirmed email before they can log on.
          var personID = sessionServices.Login(loginRequest);
            if (true)
            {
                HttpContext.Session.SetString("personId", personID.ToString());
                HttpContext.Session.SetString("emailId", loginRequest.emailId);
            }
            return HttpContext.Session.Id;
        }

        [HttpPost]
        [Route("api/Donate")]
        public void Donate([FromBody]DonationCreateRequest donationCreateRequest)
        {
            if(HttpContext.Session.GetString("personId") != null)
            {
                var personId = HttpContext.Session.GetString("personId");
                try
                {
                    donationServices.Donate(donationCreateRequest, personId);
                    
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Wrong person ID. Please check again.");
                }
            }
        }

        // GET api/person
        [HttpGet]
        [Route("api/Donate")]
        public ActionResult<IEnumerable<Donation>> Get()
        {
            return donationServices.GetAllDonation();
        }

        [HttpGet("/r/donate/{id}")]
        public ActionResult<string> GetTotalDonation(Guid id)
        {
            return donationServices.PersonTotalDonation(id.ToString());
        }

    }
}
