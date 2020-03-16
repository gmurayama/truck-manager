using Truckmanager.Domain;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class AtualizarCadastroDoMotorista
    {
        public class Command
        {
            public string MotoristaId { get; set; }

            public string Nome { get; set; }

            public Sexo Sexo { get; set; }

            public int Idade { get; set; }

            public TipoCnh TipoCnh { get; set; }
        }
    }
}
