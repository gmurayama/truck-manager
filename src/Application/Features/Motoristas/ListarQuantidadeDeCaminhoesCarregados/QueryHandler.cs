using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class ListarQuantidadeDeCaminhoesCarregados
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
                var registroCollection = _database.GetCollectionAsQueryable<Registro>();

                var result = await registroCollection
                    .Where(r => r.EstaCarregado && query.DataInicial <= r.Data && r.Data <= query.DataFinal)
                    .CountAsync();

                return result;
            }
        }
    }
}
