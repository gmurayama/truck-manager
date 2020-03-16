using Mongo2Go;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Persistence.MongoDB;
using static TruckManager.Application.Features.Registros.ListarOrigensDestinos;

namespace ApplicationTests.Features.Registros.ListarOrigensDestinos
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public async Task OneUniqueDestinationAndOneUniqueOrigin_ReturnOneLocationInDestionationAndOrigin()
        {
            using var runner = MongoDbRunner.Start();
            var database = new DatabaseService(runner.ConnectionString, "OneUniqueDestinationAndOneUniqueOrigin");
            var motoristaCollection = database.GetCollection<Motorista>();
            var registroCollection = database.GetCollection<Registro>();

            var motoristas = new List<Motorista>()
            {
                new Motorista { Id = "507f191e810c19729de860ea", Cpf = "123.456.789-00", PossuiVeiculoProprio = true },
                new Motorista { Id = "7f19192e190029029d986dbb", Cpf = "987.423.123-03", PossuiVeiculoProprio = true }
            };

            var registros = new List<Registro>()
            {
                new Registro
                {
                    MotoristaId = "507f191e810c19729de860ea",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = new Local { Nome = "Local 1", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 10, 10 } } },
                    Destino = new Local { Nome = "Local 2", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 15, 25 } } }
                },
                new Registro
                {
                    MotoristaId = "7f19192e190029029d986dbb",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = new Local { Nome = "Local 1", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 10, 10 } } },
                    Destino = new Local { Nome = "Local 2", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 15, 25 } } }
                },
                new Registro
                {
                    MotoristaId = "507f191e810c19729de860ea",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = new Local { Nome = "Local 1", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 10, 10 } } },
                    Destino = new Local { Nome = "Local 2", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 15, 25 } } }
                }
            };

            await Task.WhenAll(motoristaCollection.InsertManyAsync(motoristas), registroCollection.InsertManyAsync(registros));

            var query = new Query();

            var handler = new QueryHandler(database);
            var result = await handler.Handle(query);
            Assert.AreEqual(1, result.Origens.Count);
            Assert.AreEqual(1, result.Destinos.Count);
        }

        [Test]
        public async Task TwoUniqueDestinationAndOneUniqueOrigin_ReturnTwoLocationsInDestionationAndOneInOrigin()
        {
            using var runner = MongoDbRunner.Start();
            var database = new DatabaseService(runner.ConnectionString, "TwoUniqueDestinationAndOneUniqueOrigin");
            var motoristaCollection = database.GetCollection<Motorista>();
            var registroCollection = database.GetCollection<Registro>();

            var motoristas = new List<Motorista>()
            {
                new Motorista { Id = "507f191e810c19729de860ea", Cpf = "123.456.789-00", PossuiVeiculoProprio = true },
                new Motorista { Id = "7f19192e190029029d986dbb", Cpf = "987.423.123-03", PossuiVeiculoProprio = true }
            };

            var locais = new Local[]
            {
                new Local { Nome = "Local 1", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 15, 25 } } },
                new Local { Nome = "Local 2", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 10, 2 } } },
                new Local { Nome = "Local 3", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 30, 0 } } },
            };

            var registros = new List<Registro>()
            {
                new Registro
                {
                    MotoristaId = "507f191e810c19729de860ea",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = locais[0],
                    Destino = locais[1]
                },
                new Registro
                {
                    MotoristaId = "7f19192e190029029d986dbb",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = locais[0],
                    Destino = locais[2]
                },
                new Registro
                {
                    MotoristaId = "507f191e810c19729de860ea",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = locais[0],
                    Destino = locais[1]
                }
            };

            await Task.WhenAll(motoristaCollection.InsertManyAsync(motoristas), registroCollection.InsertManyAsync(registros));

            var query = new Query();

            var handler = new QueryHandler(database);
            var result = await handler.Handle(query);
            Assert.AreEqual(1, result.Origens.Count);
            Assert.AreEqual(2, result.Destinos.Count);
        }
    }
}
