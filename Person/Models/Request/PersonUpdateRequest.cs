using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Person.Models.Request
{
    public class PersonUpdateRequest
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public Person.Gender gender { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
