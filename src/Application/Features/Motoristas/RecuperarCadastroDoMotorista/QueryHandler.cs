using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class RecuperarCadastroDoMotorista
    {
        public class QueryHandler : IHandler<Query, Task<Motorista>>
        {
            private readonly IMongoDBService _database;

            public QueryHandler(IMongoDBService database)
            {
                _database = database;
            }

            public Task<Motorista> Handle(Query query)
            {
                var collection = _database.GetCollection<Motorista>();
                return collection.AsQueryable().SingleOrDefaultAsync(m => m.Cpf == query.Cpf);
            }
        }
    }
}
