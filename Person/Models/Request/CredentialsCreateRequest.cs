using System;
namespace PersonDocument.Models.Request
{
    public class CredentialsCreateRequest
    {
        public string emailId { get; set; }
        public string password { get; set; }
    }
}
