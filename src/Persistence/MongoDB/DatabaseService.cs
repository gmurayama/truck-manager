using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Persistence.MongoDB.Mapping;

namespace Persistence.MongoDB
{
    public class DatabaseService
    {
        private readonly IMongoClient _client;
        private readonly string _database;

        public DatabaseService(string connectionString, string database)
        {
            _client = new MongoClient(connectionString);
            _database = database;

            RegisterConventions();
            RegisterMappingClasses();
        }

        private void RegisterMappingClasses()
        {
            new MappingMotorista();
            new MappingRegistro();
        }

        private void RegisterConventions()
        {
            var pack = new ConventionPack();
            pack.Add(new CamelCaseElementNameConvention());
            ConventionRegistry.Register("camelCase", pack, t => true);
        }

        public IMongoDatabase GetInstance(MongoDatabaseSettings settings = null) => 
            _client.GetDatabase(_database, settings);
    }
}
