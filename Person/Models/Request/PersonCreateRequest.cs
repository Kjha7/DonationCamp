using System;

namespace PersonDocument.Models.Request
{
    public class PersonCreateRequest
    {

        public string firstName { get; set; }
        public DateTime GraduationDate { get; set; }
        public string emailId { get; set; }
        public Person.Gender gender { get; set; }
        public string password { get; set; }
    }
}