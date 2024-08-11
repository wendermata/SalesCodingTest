using Infra.Mongo.Settings;
using MongoDB.Driver;

namespace Infra.Mongo
{
    public interface IMongoService
    {
        IMongoSettings Settings { get; set; }
        IMongoClient Client { get; set; }
        IMongoDatabase Database { get; set; }
    }
}
