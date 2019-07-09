using System;
namespace PersonDocument.Models.Request
{
    public class CredentialsCreateRequest
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
        public Guid PersonId { get; set; }

        public CredentialsCreateRequest(Person person)
        {
            PersonId = person.PersonId;
            EmailId = person.EmailId;
            Password = person.Password;
        }
    }
}
