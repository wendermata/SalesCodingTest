using Infra.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Mongo
{
    public class MongoService : IMongoService
    {
        public IMongoSettings Settings { get; set; }
        public IMongoClient Client { get; set; }
        public IMongoDatabase Database { get; set; }

        public MongoService(IOptions<MongoSettings> settings)
        {
            Settings = settings.Value;
            Client = new MongoClient(Settings.ConnectionString);
            Database = Client.GetDatabase(Settings.DatabaseName);
        }
    }
}
