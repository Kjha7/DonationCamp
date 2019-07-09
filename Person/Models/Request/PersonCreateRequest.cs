using System;

namespace PersonDocument.Models.Request
{
    public class PersonCreateRequest
    {
        public string FirstName { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string EmailId { get; set; }
        public Person.Gender gender { get; set; }
        public string Password { get; set; }
    }
}