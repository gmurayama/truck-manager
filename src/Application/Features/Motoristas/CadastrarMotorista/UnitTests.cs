using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Truckmanager.Domain;
using TruckManager.Application.Features.Registros;
using TruckManager.Application.Persistence;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class CadastrarMotorista
    {
        [TestFixture]
        public class UnitTests
        {
            [Test]
            public async Task AddNewDriver_InsertSuccessfull()
            {
                var mockSession = new Mock<IClientSessionHandle>();
                mockSession
                    .Setup(x => x.StartTransaction(It.IsAny<TransactionOptions>()));
                mockSession
                    .Setup(x => x.AbortTransaction(It.IsAny<CancellationToken>()));
                mockSession
                    .Setup(x => x.CommitTransaction(It.IsAny<CancellationToken>()));

                var mockMotoristaCollection = new Mock<IMongoCollection<Motorista>>();
                mockMotoristaCollection
                    .Setup(x => x.InsertOneAsync(It.IsAny<Motorista>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

                var mockDatabase = new Mock<IMongoDBService>();
                mockDatabase
                    .Setup(x => x.Instance.Client.StartSession(
                        It.IsAny<ClientSessionOptions>(),
                        It.IsAny<CancellationToken>()
                    ))
                    .Returns(mockSession.Object);

                mockDatabase
                    .Setup(x => x.GetCollection<Motorista>())
                    .Returns(mockMotoristaCollection.Object);

                var queryableList = new List<Motorista>().AsQueryable();

                var mongoQueryableMock = new Mock<IMongoQueryable<Motorista>>();
                mongoQueryableMock.As<IQueryable<Motorista>>().Setup(x => x.Provider).Returns(queryableList.Provider);
                mongoQueryableMock.As<IQueryable<Motorista>>().Setup(x => x.Expression).Returns(queryableList.Expression);
                mongoQueryableMock.As<IQueryable<Motorista>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
                mongoQueryableMock.As<IQueryable<Motorista>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

                mockDatabase
                    .Setup(x => x.GetCollectionAsQueryable<Motorista>())
                    .Returns(mongoQueryableMock.Object);

                var mockHandler = new Mock<IHandler<RegistrarPassagemPeloTerminal.Command, Task>>();
                mockHandler
                    .Setup(x => x.Handle(It.IsAny<RegistrarPassagemPeloTerminal.Command>()))
                    .Returns(Task.CompletedTask);

                var command = new Command
                {
                    Cpf = "123.456.789-12",
                    Destino = new Local 
                    {
                        Localizacao = new Localizacao { Coordinates = new double[] { 10, 10 } }
                    },
                    Origem = new Local 
                    {
                        Localizacao = new Localizacao { Coordinates = new double[] { 20, 20 } }
                    },
                    EstaCarregado = false,
                    Idade = 18,
                    Nome = "Test",
                    Sexo = Sexo.Masculino,
                    TipoCaminhao = TipoCaminhao.CaminhaoTresQuartos,
                    TipoCnh = TipoCnh.A
                };

                var handler = new CommandHandler(mockDatabase.Object, mockHandler.Object);
                var resolved = await handler.Handle(command);

                mockSession.Verify(x => x.CommitTransaction(It.IsAny<CancellationToken>()), Times.Once);
                mockSession.Verify(x => x.AbortTransaction(It.IsAny<CancellationToken>()), Times.Never);
                Assert.IsTrue(resolved.IsOk);
            }

            [Test]
            public async Task AddExistingDriver_InsertUnsuccessfull()
            {
                var mockSession = new Mock<IClientSessionHandle>();
                mockSession
                    .Setup(x => x.StartTransaction(It.IsAny<TransactionOptions>()));
                mockSession
                    .Setup(x => x.AbortTransaction(It.IsAny<CancellationToken>()));
                mockSession
                    .Setup(x => x.CommitTransaction(It.IsAny<CancellationToken>()));

                var mockMotoristaCollection = new Mock<IMongoCollection<Motorista>>();
                mockMotoristaCollection
                    .Setup(x => x.InsertOneAsync(It.IsAny<Motorista>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

                var mockDatabase = new Mock<IMongoDBService>();
                mockDatabase
                    .Setup(x => x.Instance.Client.StartSession(
                        It.IsAny<ClientSessionOptions>(),
                        It.IsAny<CancellationToken>()
                    ))
                    .Returns(mockSession.Object);

                mockDatabase
                    .Setup(x => x.GetCollection<Motorista>())
                    .Returns(mockMotoristaCollection.Object);

                var list = new Motorista[] 
                {
                    new Motorista { Cpf = "123.456.789-12" }
                };
                var queryableList = list.AsQueryable();

                var mongoQueryableMock = new Mock<IMongoQueryable<Motorista>>();
                mongoQueryableMock.As<IQueryable<Motorista>>().Setup(x => x.Provider).Returns(queryableList.Provider);
                mongoQueryableMock.As<IQueryable<Motorista>>().Setup(x => x.Expression).Returns(queryableList.Expression);
                mongoQueryableMock.As<IQueryable<Motorista>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
                mongoQueryableMock.As<IQueryable<Motorista>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());

                mockDatabase
                    .Setup(x => x.GetCollectionAsQueryable<Motorista>())
                    .Returns(mongoQueryableMock.Object);

                var mockHandler = new Mock<IHandler<RegistrarPassagemPeloTerminal.Command, Task>>();
                mockHandler
                    .Setup(x => x.Handle(It.IsAny<RegistrarPassagemPeloTerminal.Command>()))
                    .Returns(Task.CompletedTask);

                var command = new Command
                {
                    Cpf = "123.456.789-12",
                    Destino = new Local
                    {
                        Localizacao = new Localizacao { Coordinates = new double[] { 10, 10 } }
                    },
                    Origem = new Local
                    {
                        Localizacao = new Localizacao { Coordinates = new double[] { 20, 20 } }
                    },
                    EstaCarregado = false,
                    Idade = 18,
                    Nome = "Test",
                    Sexo = Sexo.Masculino,
                    TipoCaminhao = TipoCaminhao.CaminhaoTresQuartos,
                    TipoCnh = TipoCnh.A
                };

                var handler = new CommandHandler(mockDatabase.Object, mockHandler.Object);
                var resolved = await handler.Handle(command);

                mockSession.Verify(x => x.CommitTransaction(It.IsAny<CancellationToken>()), Times.Never);
                mockSession.Verify(x => x.AbortTransaction(It.IsAny<CancellationToken>()), Times.Never);
                Assert.IsTrue(resolved.IsErr);
            }
        }
    }
}
