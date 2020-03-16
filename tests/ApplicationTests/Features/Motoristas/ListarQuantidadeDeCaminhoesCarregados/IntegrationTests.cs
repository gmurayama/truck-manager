using Mongo2Go;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Persistence.MongoDB;
using static TruckManager.Application.Features.Motoristas.ListarQuantidadeDeCaminhoesCarregados;

namespace ApplicationTests.Features.Motoristas.ListarQuantidadeDeCaminhoesCarregados
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public async Task AllTrucksWereLoaded_ReturnThree()
        {
            using (var runner = MongoDbRunner.Start())
            {
                var database = new DatabaseService(runner.ConnectionString, "AllTrucksWereLoaded_ReturnThree");
                var motoristaCollection = database.GetCollection<Motorista>();
                var registroCollection = database.GetCollection<Registro>();

                var motoristas = new List<Motorista>()
            {
                new Motorista { Id = "507f191e810c19729de860ea", Cpf = "123.456.789-00", Idade = 30, Nome = "Name", Sexo = Sexo.Masculino, TipoCnh = TipoCnh.A },
                new Motorista { Id = "40af192e110d19029d986dea", Cpf = "321.654.987-00", Idade = 35, Nome = "Firstname Lastname", Sexo = Sexo.Feminino, TipoCnh = TipoCnh.B }
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
                    MotoristaId = "507f191e810c19729de860ea",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = new Local { Nome = "Local 1", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 10, 10 } } },
                    Destino = new Local { Nome = "Local 2", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 15, 25 } } }
                },
                new Registro
                {
                    MotoristaId = "40af192e110d19029d986dea",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = new Local { Nome = "Local 1", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 10, 10 } } },
                    Destino = new Local { Nome = "Local 2", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 15, 25 } } }
                }
            };

                var addMotoristas = motoristaCollection.InsertManyAsync(motoristas);
                var addRegistros = registroCollection.InsertManyAsync(registros);

                await Task.WhenAll(addMotoristas, addRegistros);

                var query = new Query
                {
                    DataInicial = new DateTime(2020, 01, 01),
                    DataFinal = new DateTime(2020, 01, 02)
                };

                var handler = new QueryHandler(database);
                var result = await handler.Handle(query);
                Assert.AreEqual(3, result);
            }
        }

        [Test]
        public async Task QueryDateThatPossessNoRegisters_ReturnZero()
        {
            using (var runner = MongoDbRunner.Start())
            {
                var database = new DatabaseService(runner.ConnectionString, "QueryDateThatPossessNoRegisters_ReturnZero");
                var motoristaCollection = database.GetCollection<Motorista>();
                var registroCollection = database.GetCollection<Registro>();

                var motoristas = new List<Motorista>()
            {
                new Motorista { Id = "507f191e810c19729de860ea", Cpf = "123.456.789-00", Idade = 30, Nome = "Name", Sexo = Sexo.Masculino, TipoCnh = TipoCnh.A },
                new Motorista { Id = "40af192e110d19029d986dea", Cpf = "321.654.987-00", Idade = 35, Nome = "Firstname Lastname", Sexo = Sexo.Feminino, TipoCnh = TipoCnh.B }
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
                    MotoristaId = "507f191e810c19729de860ea",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = new Local { Nome = "Local 1", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 10, 10 } } },
                    Destino = new Local { Nome = "Local 2", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 15, 25 } } }
                },
                new Registro
                {
                    MotoristaId = "40af192e110d19029d986dea",
                    Data = new DateTime(2020, 01, 01),
                    EstaCarregado = true,
                    TipoCaminhao = TipoCaminhao.CaminhaoTruck,
                    Origem = new Local { Nome = "Local 1", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 10, 10 } } },
                    Destino = new Local { Nome = "Local 2", Localizacao = new Localizacao { Type = "Point", Coordinates = new double[] { 15, 25 } } }
                }
            };

                var addMotoristas = motoristaCollection.InsertManyAsync(motoristas);
                var addRegistros = registroCollection.InsertManyAsync(registros);

                await Task.WhenAll(addMotoristas, addRegistros);

                var query = new Query
                {
                    DataInicial = new DateTime(2021, 01, 01),
                    DataFinal = new DateTime(2021, 01, 02)
                };

                var handler = new QueryHandler(database);
                var result = await handler.Handle(query);
                Assert.AreEqual(0, result);
            }
        }
    }
}
