using System;
using DonationCamp.Models.Request;

namespace DonationCamp.Configs
{
    public class MongoDbConfig
    {
        public string Uri { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
}
