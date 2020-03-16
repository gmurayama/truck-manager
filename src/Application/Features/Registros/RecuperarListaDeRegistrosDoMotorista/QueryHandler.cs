using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Registros
{
    public partial class RecuperarListaDeRegistrosDoMotorista
    {
        public class QueryHandler : IHandler<Query, Task<List<Registro>>>
        {
            private readonly IMongoDBService _database;

            public QueryHandler(IMongoDBService database)
            {
                _database = database;
            }

            public Task<List<Registro>> Handle(Query query)
            {
                var registroCollection = _database.GetCollection<Registro>();

                int page = query.Page ?? 1;
                int pageSize = query.PageSize ?? 10;

                return registroCollection.AsQueryable()
                    .Where(r => r.MotoristaId == query.MotoristaId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
        }
    }
}
