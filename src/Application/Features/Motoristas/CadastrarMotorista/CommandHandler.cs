using MonadicResponseHandler;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Features.Registros;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class CadastrarMotorista
    {
        public class CommandHandler
        {
            private readonly IMongoDBService _database;
            private readonly IHandler<RegistrarPassagemPeloTerminal.Command, Task> _registrarPassagemPeloTerminalHandler;

            public CommandHandler(IMongoDBService database,
                 IHandler<RegistrarPassagemPeloTerminal.Command, Task> registrarPassagemPeloTerminalHandler)
            {
                _database = database;
                _registrarPassagemPeloTerminalHandler = registrarPassagemPeloTerminalHandler;
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

                        var registrarPassagemPeloTerminalCommand = new RegistrarPassagemPeloTerminal.Command
                        {
                            MotoristaId = motorista.Id,
                            EstaCarregado = command.EstaCarregado,
                            TipoCaminhao = command.TipoCaminhao,
                            Origem = command.Origem,
                            Destino = command.Destino
                        };

                        await _registrarPassagemPeloTerminalHandler.Handle(registrarPassagemPeloTerminalCommand);

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
