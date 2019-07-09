using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDocument.Models.Request
{
    public class PersonUpdateRequest
    {
        public string FirstName { get; set; }
        public DateTime GraduationDate { get; set; }
        public string EmailId { get; set; }
        public Person.Gender gender { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
