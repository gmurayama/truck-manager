using Mongo2Go;
using MongoDB.Driver;
using System.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Features.Registros;
using TruckManager.Persistence.MongoDB;
using static TruckManager.Application.Features.Motoristas.CadastrarMotorista;

namespace ApplicationTests.Features.Motoristas.CadastrarMotorista
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public async Task InsertNewDriver_Sucessfull()
        {
            using var runner = MongoDbRunner.Start();
            var database = new DatabaseService(runner.ConnectionString, "InsertNewDriver_Sucessfull");

            var registroHandler = new RegistrarPassagemPeloTerminal.CommandHandler(database);

            var command = new Command 
            {
                Cpf = "123.456.789-00",
                Destino = new Local { Type = "Point", Coordinates = new double[] { 10, 10 } },
                Origem = new Local { Type = "Point", Coordinates = new double[] { 20, 20 } },
                EstaCarregado = false,
                Idade = 20,
                Nome = "New Driver",
                Sexo = Sexo.Masculino,
                TipoCaminhao = TipoCaminhao.CaminhaoToco,
                TipoCnh = TipoCnh.D
            };

            var handler = new CommandHandler(database, registroHandler);
            var resolved = await handler.Handle(command);
            Assert.IsTrue(resolved.IsOk);

            var collection = database.GetCollectionAsQueryable<Motorista>();
            var motorista = collection.SingleOrDefault(m => m.Cpf == "123.456.789-00");

            Assert.IsNotNull(motorista);
            Assert.AreEqual(command.Nome, motorista.Nome);
        }

        [Test]
        public async Task InsertAlreadyExistingDriver_Err()
        {
            using var runner = MongoDbRunner.Start();
            var database = new DatabaseService(runner.ConnectionString, "InsertAlreadyExistingDriver_Err");
            var collection = database.GetCollection<Motorista>();
            await collection.InsertOneAsync(new Motorista
            {
                Id = "40af192e110d19029d986dea",
                Cpf = "123.456.789-00",
                Idade = 20,
                Nome = "Firstname Lastname",
                Sexo = Sexo.Feminino,
                TipoCnh = TipoCnh.D
            });

            var registroHandler = new RegistrarPassagemPeloTerminal.CommandHandler(database);

            var command = new Command
            {
                Cpf = "123.456.789-00",
                Destino = new Local { Type = "Point", Coordinates = new double[] { 10, 10 } },
                Origem = new Local { Type = "Point", Coordinates = new double[] { 20, 20 } },
                EstaCarregado = false,
                Idade = 20,
                Nome = "New Driver",
                Sexo = Sexo.Masculino,
                TipoCaminhao = TipoCaminhao.CaminhaoToco,
                TipoCnh = TipoCnh.D
            };

            var handler = new CommandHandler(database, registroHandler);
            var resolved = await handler.Handle(command);
            Assert.IsTrue(resolved.IsErr);

            var count = database.GetCollectionAsQueryable<Registro>().Count();
            Assert.AreEqual(0, count);
        }
    }
}
