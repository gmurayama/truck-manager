using Truckmanager.Domain;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace TruckManager.Persistence.MongoDB.Mapping
{
    public class MappingMotorista
    {
        public MappingMotorista()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Motorista)))
            {
                BsonClassMap.RegisterClassMap<Motorista>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(c => c.Id).SetIdGenerator(StringObjectIdGenerator.Instance);
                    cm.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId));
                    cm.MapMember(c => c.TipoCnh).SetSerializer(new EnumSerializer<TipoCnh>(BsonType.String));
                    cm.MapMember(c => c.Sexo).SetSerializer(new EnumSerializer<Sexo>(BsonType.String));
                });
            }
        }
    }
}
