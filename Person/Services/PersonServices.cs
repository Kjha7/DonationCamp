namespace DefaultNamespace
{
    public class PersonServices :IPersonService
    {
        private readonly IMongoCollection<PersonDocument> _person;

        public PersonServices() { }
        
        public PersonServices(PersonDocument personDocument)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _person = database.GetCollection<Person>(settings.PersonDocument);
        }

        public List<Person> GetAllPersons()
        {
            //Functon will return PersonCreateRequest, so new person is created.
            return _person.AsQueryable().ToList().Select(prop => new Person(p)).ToList();
        }

        public Person GetPerson(Guid personId)
        {
            try
            {
                var personDocument = _person.Find(p => p.id == personId).SingleAsync().Result;
                return new Person(personDocument);
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public void DeletePerson(Guid personId)
        {
            try
            {
                var personDocument = _person.Find(p => p.id == personId).SingleAsync().Result;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public Person CreatePerson(PersonCreateRequest personCreateRequest)
        {
            var personDocument = new PersonDocument(personCreateRequest);
            _person.InsertOneAsync(personDocument).Wait();
            return new Person(personDocument);
        }

        public Person UpdatePerson(Guid personId, PersonUpdateRequest personUpdateRequest)
        {
            var personDocument = _person.FindOneAndUpdateAsync(p => p.id == personId).Result;
            var updatedAt = DateTime.UtcNow;
            return new Person(personDocument.updatePerson(personUpdateRequest, updatedAt));

        }
    }
}