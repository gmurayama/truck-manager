using Truckmanager.Domain;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class CadastrarMotorista
    {
        public class Command
        {
            public string Nome { get; set; }

            public int Idade { get; set; }

            public string Cpf { get; set; }

            public Sexo Sexo { get; set; }

            public TipoCnh TipoCnh { get; set; }

            public TipoCaminhao TipoCaminhao { get; set; }

            public bool EstaCarregado { get; set; }

            public Local Origem { get; set; }

            public Local Destino { get; set; }
        }
    }
}
