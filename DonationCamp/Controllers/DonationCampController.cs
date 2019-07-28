/*
 * [http_method, http_url, http_status, instance, environment]
 * Process time per service [Histogram]
 * Numbe of requests came in 
 * Number of user logged in per day/today/week [counter]
 * Each user logged in count per hr/today/day/week [counter]
 * Person Total donation per day/today [Gauge/Histogram]
 * revenue per cpu usage [Gauge/Histogram]
 * % wise users' contribution [Histogram]
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DonationCamp.Services;
using DonationCamp.Models.Request;
using Microsoft.AspNetCore.Http;
using DonationCamp.Models.Entity;
using DonationCamp.Models.Response;
using Prometheus;

namespace DonationCamp.Controllers
{
    [Route("/")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        public DonationServices donationServices;
        public SessionServices sessionServices;
  
        private static readonly Histogram LoginDuration
                = Metrics.CreateHistogram("services_duration_seconds", "Histogram of services processing durations.",
                    new HistogramConfiguration
                    {
                        LabelNames = new[] { "action" }
                    });

        public DonationController(DonationServices donation, SessionServices _sessionServices)
        {
            donationServices = donation;
            sessionServices = _sessionServices;
        }

        // POST account/Login
        [HttpPost]
        [Route("account/Login")]
        public ActionResult<SessionResponse> Login([FromBody]LoginRequest loginRequest)
        {
            Guid personID;
            using (LoginDuration.WithLabels("Login").NewTimer())
            {
                 personID = sessionServices.Login(loginRequest);
            // Require the user to have a confirmed email before they can log on.
            
                if (true)
                {
                    HttpContext.Session.SetString("personId", personID.ToString());
                    HttpContext.Session.SetString("emailId", loginRequest.EmailId);
                }
            }
            return new SessionResponse(HttpContext.Session.Id, personID);
        }

        [HttpGet]
        [Route("account/session")]
        public ActionResult<string> GetSessionInfo()
        {
                return HttpContext.Session.Id;
        }

        //[HttpPost]
        //[Route("account/Logout")]
        //public string Logout()
        //{
        //    // Require the user to have a confirmed email before they can log on.
        //    var SessionClose = HttpContext.Session.Id;

        //    if (true)
        //    {
        //        HttpContext.Session.Remove(SessionClose);

        //    }
        //    return "Session Closed for "+ SessionClose;
        //}

        [HttpPost]
        [Route("api/Donate")]
        public string Donate([FromBody]DonationCreateRequest donationCreateRequest)
        {
            if (HttpContext.Session.GetString("personId") != null)
            {
                var personId = HttpContext.Session.GetString("personId");
                try
                {
                    using(LoginDuration.WithLabels("Donate").NewTimer()) {
                        donationServices.Donate(donationCreateRequest, personId);
                    }
                    return "Thank You :)";
                    
                }
                catch (Exception)
                {
                    System.Console.WriteLine("Wrong person ID. Please check again.");
                }
            }
            return "No user logged In. Please login first";
        }

        // GET api/person
        [HttpGet]
        [Route("api/Donate")]
        public ActionResult<IEnumerable<Donation>> Get()
        {
            int TotalDonation = 0;
            ActionResult<IEnumerable<Donation>> Totaldonationlist = null;
            using (LoginDuration.WithLabels("Get").NewTimer())
            {
                 Totaldonationlist = donationServices.GetAllDonation();
                
                foreach (Donation d in Totaldonationlist.Value)
                {
                    TotalDonation = (int)(TotalDonation + d.Amt);
                }
            }
            return Totaldonationlist;
            //return donationServices.GetAllDonation();
        }

        [HttpGet("/r/donate/{id}")]
        public ActionResult<string> GetTotalDonation(Guid id)
        {
            ActionResult<string> personDonation = null;
            using (LoginDuration.WithLabels("Get").NewTimer())
            {
                personDonation = donationServices.PersonTotalDonation(id.ToString());
            }
            return personDonation;
        }

    }
}
