using System;

namespace Person.Models.Request
{
    public class PersonCreateRequest
    {

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public Person.Gender gender { get; set; }
    }
}