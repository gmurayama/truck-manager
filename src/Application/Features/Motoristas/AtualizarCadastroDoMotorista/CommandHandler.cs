using MonadicResponseHandler;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class AtualizarCadastroDoMotorista
    {
        public class CommandHandler : IHandler<Command, Task<Resolved>>
        {
            private readonly IMongoDBService _database;

            public CommandHandler(IMongoDBService database)
            {
                _database = database;
            }

            public async Task<Resolved> Handle(Command command)
            {
                var collection = _database.GetCollection<Motorista>();
                var motorista = await _database.GetCollectionAsQueryable<Motorista>().SingleOrDefaultAsync(m => m.Id == command.MotoristaId);

                if (motorista == null)
                    return Resolved.ErrAsIEnumerable(new Exception("Não foi possível encontrar o cadastro do motorista solicitado"));

                motorista.Nome = command.Nome;
                motorista.Sexo = command.Sexo;
                motorista.Idade = command.Idade;
                motorista.TipoCnh = command.TipoCnh;
                motorista.PossuiVeiculoProprio = command.PossuiVeiculoProprio;

                await collection.ReplaceOneAsync(m => m.Id == command.MotoristaId, motorista);
                return Resolved.Ok();
            }
        }
    }
}
