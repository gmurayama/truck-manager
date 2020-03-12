using Truckmanager.Domain;

namespace TruckManager.Application.Features.Registros
{
    public partial class RegistrarPassagemPeloTerminal
    {
        public class Command
        {
            public string MotoristaId { get; set; }

            public bool EstaCarregado { get; set; }

            public TipoCaminhao TipoCaminhao { get; set; }

            public Local Origem { get; set; }

            public Local Destino { get; set; }
        }
    }
}
