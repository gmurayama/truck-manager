using MonadicResponseHandler;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class CadastrarMotorista
    {
        public class CommandHandler
        {
            private readonly IMongoDBService _database;

            public CommandHandler(IMongoDBService database)
            {
                _database = database;
            }

            public async Task<Resolved> Handle(Command command)
            {
                using (var session = _database.Instance.Client.StartSession())
                {
                    try
                    {
                        session.StartTransaction();

                        var motoristaCollection = _database.GetCollection<Motorista>();
                        var registroCollection = _database.GetCollection<Registro>();

                        if (motoristaCollection.AsQueryable().Any(m => m.Cpf == command.Cpf))
                            return Resolved.ErrAsIEnumerable(new InvalidOperationException("Já existe um motorista cadastrado com esse CPF"));

                        var motorista = new Motorista
                        {
                            Cpf = command.Cpf,
                            Idade = command.Idade,
                            Nome = command.Nome,
                            Sexo = command.Sexo,
                            TipoCnh = command.TipoCnh
                        };

                        await motoristaCollection.InsertOneAsync(motorista);

                        var registro = new Registro
                        {
                            MotoristaId = motorista.Id,
                            EstaCarregado = command.EstaCarregado,
                            Origem = command.Origem,
                            Destino = command.Destino,
                            TipoCaminhao = command.TipoCaminhao,
                            Data = DateTime.Now
                        };

                        await registroCollection.InsertOneAsync(registro);

                        session.CommitTransaction();

                        return Resolved.Ok();
                    }
                    catch (Exception ex)
                    {
                        session.AbortTransaction();
                        return Resolved.ErrAsIEnumerable(ex);
                    }
                }
            }
        }
    }
}
