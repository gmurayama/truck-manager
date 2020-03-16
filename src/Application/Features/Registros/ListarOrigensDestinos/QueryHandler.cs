using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Registros
{
    public partial class ListarOrigensDestinos
    {
        public class QueryHandler : IHandler<Query, Task<ResponseModel>>
        {
            private readonly IMongoDBService _database;

            public QueryHandler(IMongoDBService database)
            {
                _database = database;
            }

            public async Task<ResponseModel> Handle(Query query)
            {
                var collection = _database.GetCollectionAsQueryable<Registro>();

                var taskOrigens = collection
                    .GroupBy(r => r.Origem.Nome)
                    .Select(r => r.Key)
                    .ToListAsync();

                var taskDestinos = collection
                    .GroupBy(r => r.Destino.Nome)
                    .Select(r => r.Key)
                    .ToListAsync();

                await Task.WhenAll(taskOrigens, taskDestinos);

                return new ResponseModel
                {
                    Origens = taskOrigens.Result,
                    Destinos = taskDestinos.Result
                };
            }
        }
    }
}
