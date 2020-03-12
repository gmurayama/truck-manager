using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Persistence.MongoDB.Mapping;
using TruckManager.Application.Persistence;

namespace Persistence.MongoDB
{
    public class DatabaseService : IMongoDBService
    {
        private readonly IMongoClient _client;

        public DatabaseService(string connectionString, string database, MongoDatabaseSettings settings = null)
        {
            _client = new MongoClient(connectionString);
            Instance = _client.GetDatabase(database, settings);

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

        public IMongoDatabase Instance { get; }

        public IMongoCollection<T> GetCollection<T>() => Instance.GetCollection<T>(typeof(T).Name);
    }
}
