using Truckmanager.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace TruckManager.Persistence.MongoDB.Mapping
{
    public class MappingRegistro
    {
        public MappingRegistro()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Registro)))
            {
                BsonClassMap.RegisterClassMap<Registro>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                    cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                    cm.MapMember(c => c.MotoristaId).SetSerializer(new StringSerializer(BsonType.ObjectId));
                });
            }
        }
    }
}
