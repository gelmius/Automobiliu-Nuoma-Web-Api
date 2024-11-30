using MongoDB.Driver;

namespace Automobiliu_Nuoma_Web_Api.Services
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase? _database;

        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            var connectionString = _configuration.GetConnectionString("MongoConnection");
            var mongoUrl = MongoUrl.Create(connectionString);
            var mobgoClient = new MongoClient(mongoUrl);
            _database = mobgoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? Database => _database;
    }
}
