namespace DefaultNamespace
{
    public class PersonDocument
    {

        public PersonDocument(PersonCreateRequest personCreateRequest)
        {
            id = personCreateRequest.id;
            firstName = personCreateRequest.firstName;
            lastName = personCreateRequest.lastName;
            emailId = personCreateRequest.emailId;
            gender = personCreateRequest.gender;
        }
        
        [BsonId]
        private Guid id { get; set; }
        private string firstName { get; set; }
        private string lastName { get; set; }
        private string emailId { get; set; }
        private Gender gender { get; set; }
        
        public enum Gender
        {
            Male,
            Female,
            Unknown
        }
    }
}