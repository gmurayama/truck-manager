using MongoDB.Driver;
using System.Linq;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class ListarQuantidadeDeCaminhoesCarregados
    {
        public class QueryHandler : IHandler<Query, int>
        {
            private readonly IMongoDBService _database;

            public QueryHandler(IMongoDBService database)
            {
                _database = database;
            }

            public int Handle(Query query)
            {
                var registroCollection = _database.GetCollection<Registro>();

                var result = registroCollection
                    .AsQueryable()
                    .Where(r => r.EstaCarregado && query.DataInicial <= r.Data && r.Data <= query.DataFinal)
                    .Count();

                return result;
            }
        }
    }
}
