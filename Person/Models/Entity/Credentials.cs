using System;
using PersonDocument.Models.Request;

namespace PersonDocument.Models.Entity
{
    public class Credentials
    {
        public Credentials(CredentialsCreateRequest credentialsCreateRequest)
        {
            emailId = credentialsCreateRequest.emailId;
            password = credentialsCreateRequest.password;
        }
        public Credentials() { }

        public string emailId { get; set; }
        public string password { get; set; }
    }
}
