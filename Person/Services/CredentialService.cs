using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PersonDocument.Configs;
using PersonDocument.Models.Entity;
using PersonDocument.Models.Request;

namespace PersonDocument.Services
{
    public class CredentialService
    {
        private LoginConfig _loginconfig;
        private IMongoCollection<Credentials> _credentials;

        public CredentialService(IOptions<LoginConfig> settings)
        {
            _loginconfig = settings.Value;
            var client = new MongoClient(_loginconfig.Uri);
            var database = client.GetDatabase(_loginconfig.Database);

            _credentials = database.GetCollection<Credentials>(_loginconfig.Collection);
        }

        public void CreateCredentials(CredentialsCreateRequest credentialsCreateRequest)
        {
            var cred = new Credentials(credentialsCreateRequest);
            _credentials.InsertOneAsync(cred).Wait();
        }

        public void DeleteCredentials(Guid id)
        {
            _credentials.DeleteManyAsync(id.ToString()).Wait();
        }
    }
}
