using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;
using MongoDB.Driver;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class ConsultarMotoristasSemCarga
    {
        public class QueryHandler : IHandler<Query, Task<List<ResponseModel>>>
        {
            private readonly IMongoDBService _database;

            public QueryHandler(IMongoDBService database)
            {
                _database = database;
            }

            public async Task<List<ResponseModel>> Handle(Query query)
            {
                var registroCollection = _database.GetCollection<Registro>();
                var motoristaCollection = _database.GetCollection<Motorista>();

                var result = await registroCollection
                    .AsQueryable()
                    .Where(r => !r.EstaCarregado && r.Data >= query.DataInicial && r.Data <= query.DataFinal)
                    .Join(motoristaCollection,
                          registro => registro.MotoristaId,
                          motorista => motorista.Id,
                          (registro, motorista) => new { Registro = registro, Motorista = motorista })
                    .ToListAsync();

                var response = result.GroupBy(x => x.Motorista.Cpf,
                                             (key, values) => new ResponseModel { Motorista = values.First().Motorista, Registros = values.Select(r => r.Registro) })
                                             .ToList();

                return response;
            }
        }
    }
}
