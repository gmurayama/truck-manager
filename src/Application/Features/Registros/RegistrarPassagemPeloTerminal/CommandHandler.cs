using System;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Registros
{
    public partial class RegistrarPassagemPeloTerminal
    {
        public class CommandHandler
        {
            private readonly IMongoDBService _database;

            public CommandHandler(IMongoDBService database)
            {
                _database = database;
            }

            public Task Handle(Command command)
            {
                var registroCollection = _database.GetCollection<Registro>();
                return registroCollection.InsertOneAsync(new Registro
                {
                    MotoristaId = command.MotoristaId,
                    Origem = command.Origem,
                    Destino = command.Destino,
                    EstaCarregado = command.EstaCarregado,
                    TipoCaminhao = command.TipoCaminhao,
                    Data = DateTime.Now
                });
            }
        }
    }
}
