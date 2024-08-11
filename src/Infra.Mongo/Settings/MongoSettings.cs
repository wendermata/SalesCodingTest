namespace Infra.Mongo.Settings
{
    public class MongoSettings : IMongoSettings
    {
        public string ProductCollectionName { get; set; }
        public string SalesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoSettings
    {
        string ProductCollectionName { get; set; }
        string SalesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}