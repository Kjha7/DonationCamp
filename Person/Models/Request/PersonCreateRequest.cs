namespace DefaultNamespace
{
    public class PersonCreateRequest
    {
        
        public Guid id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailId { get; set; }
        public Gender gender { get; set; }
    }
}