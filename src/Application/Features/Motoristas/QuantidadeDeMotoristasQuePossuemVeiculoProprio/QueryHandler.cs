using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class QuantidadeDeMotoristasQuePossuemVeiculoProprio
    {
        public class QueryHandler : IHandler<Query, Task<int>>
        {
            private readonly IMongoDBService _database;

            public QueryHandler(IMongoDBService database)
            {
                _database = database;
            }

            public async Task<int> Handle(Query query)
            {
                var collection = _database.GetCollectionAsQueryable<Motorista>();
                return await collection.Where(x => x.PossuiVeiculoProprio).CountAsync();
            }
        }
    }
}
