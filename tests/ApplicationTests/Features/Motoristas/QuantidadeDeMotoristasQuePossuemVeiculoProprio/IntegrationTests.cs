using Mongo2Go;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Persistence.MongoDB;
using static TruckManager.Application.Features.Motoristas.QuantidadeDeMotoristasQuePossuemVeiculoProprio;

namespace ApplicationTests.Features.Motoristas.QuantidadeDeMotoristasQuePossuemVeiculoProprio
{

    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public async Task TwoDriversWithVehicle_ReturnTwo()
        {
            using var runner = MongoDbRunner.Start();
            var database = new DatabaseService(runner.ConnectionString, "TwoDriversWithVehicle_ReturnTwo");
            var motoristaCollection = database.GetCollection<Motorista>();
            var registroCollection = database.GetCollection<Registro>();

            var motoristas = new List<Motorista>()
            {
                new Motorista { Id = "507f191e810c19729de860ea", Cpf = "123.456.789-00", PossuiVeiculoProprio = true },
                new Motorista { Id = "40af192e110d19029d986dea", Cpf = "321.654.987-00", PossuiVeiculoProprio = false },
                new Motorista { Id = "21af192e1fdde90292987dba", Cpf = "432.432.423-05", PossuiVeiculoProprio = false },
                new Motorista { Id = "7f19192e190029029d986dbb", Cpf = "987.423.123-03", PossuiVeiculoProprio = true }
            };

            await motoristaCollection.InsertManyAsync(motoristas);

            var query = new Query();

            var handler = new QueryHandler(database);
            var result = await handler.Handle(query);
            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task ZeroDriverWithVehicle_ReturnZero()
        {
            using var runner = MongoDbRunner.Start();
            var database = new DatabaseService(runner.ConnectionString, "TwoDriversWithVehicle_ReturnTwo");
            var motoristaCollection = database.GetCollection<Motorista>();
            var registroCollection = database.GetCollection<Registro>();

            var motoristas = new List<Motorista>()
            {
                new Motorista { Id = "507f191e810c19729de860ea", Cpf = "123.456.789-00", PossuiVeiculoProprio = false },
                new Motorista { Id = "40af192e110d19029d986dea", Cpf = "321.654.987-00", PossuiVeiculoProprio = false },
                new Motorista { Id = "21af192e1fdde90292987dba", Cpf = "432.432.423-05", PossuiVeiculoProprio = false },
                new Motorista { Id = "7f19192e190029029d986dbb", Cpf = "987.423.123-03", PossuiVeiculoProprio = false }
            };

            await motoristaCollection.InsertManyAsync(motoristas);

            var query = new Query();

            var handler = new QueryHandler(database);
            var result = await handler.Handle(query);
            Assert.AreEqual(0, result);
        }
    }
}
