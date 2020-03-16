using Mongo2Go;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using NUnit.Framework;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Persistence.MongoDB;
using static TruckManager.Application.Features.Motoristas.AtualizarCadastroDoMotorista;

namespace ApplicationTests.Features.Motoristas.AtualizarCadastroDoMorotista
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public async Task UpdateDriverInfos_Successfull()
        {
            using var _runner = MongoDbRunner.Start();
            var database = new DatabaseService(_runner.ConnectionString, "UpdateDriverInfos_Successfull");
            var collection = database.GetCollection<Motorista>();
            collection.InsertOne(new Motorista { Id = "507f191e810c19729de860ea", Cpf = "123.456.789-00", Idade = 20, Nome = "Firstname Lastname", Sexo = Sexo.Feminino, TipoCnh = TipoCnh.A });
            collection.InsertOne(new Motorista { Id = "40af192e110d19029d986dea", Cpf = "987.654.321-00", Idade = 25, Nome = "Firstname Middlename Lastname", Sexo = Sexo.Masculino, TipoCnh = TipoCnh.D });

            var command = new Command { MotoristaId = "507f191e810c19729de860ea", Idade = 20, Nome = "New name", Sexo = Sexo.Feminino, TipoCnh = TipoCnh.B };
            var handler = new CommandHandler(database);

            var result = await handler.Handle(command);
            Assert.IsTrue(result.IsOk);

            var updated = await collection.AsQueryable().SingleOrDefaultAsync(m => m.Id == "507f191e810c19729de860ea");
            Assert.AreEqual("New name", updated.Nome);
            Assert.AreEqual(Sexo.Feminino, updated.Sexo);
            Assert.AreEqual(TipoCnh.B, updated.TipoCnh);
        }

        [Test]
        public async Task UpdateInexistingDriver_OperationCancelled()
        {
            using var runner = MongoDbRunner.Start();
            var database = new DatabaseService(runner.ConnectionString, "UpdateInexistingDriver_OperationCancelled");
            var collection = database.GetCollection<Motorista>();
            collection.InsertOne(new Motorista { Id = "507f191e810c19729de860ea", Cpf = "123.456.789-00", Idade = 20, Nome = "Firstname Lastname", Sexo = Sexo.Feminino, TipoCnh = TipoCnh.A });
            collection.InsertOne(new Motorista { Id = "40af192e110d19029d986dea", Cpf = "987.654.321-00", Idade = 25, Nome = "Firstname Middlename Lastname", Sexo = Sexo.Masculino, TipoCnh = TipoCnh.D });

            var command = new Command { MotoristaId = "0123456789abcdef01234567", Idade = 20, Nome = "New name", Sexo = Sexo.Feminino, TipoCnh = TipoCnh.B };
            var handler = new CommandHandler(database);

            var result = await handler.Handle(command);
            Assert.IsTrue(result.IsErr);
        }
    }
}
