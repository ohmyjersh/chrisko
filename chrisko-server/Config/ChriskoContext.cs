using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ChrisKo.Models;

namespace ChrisKo.Config
{
    public class ChriskoContext
    {
        private readonly IMongoDatabase _database = null;

        public ChriskoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Chrisko> Chrisko
        {
            get
            {
                return _database.GetCollection<Chrisko>("Chrisko");
            }
        }
    }
}